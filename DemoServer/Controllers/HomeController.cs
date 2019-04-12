using System;
using System.Linq;
using System.Threading.Tasks;
using DemoParser.Models;
using DemoServer.Models;
using DemoServer.Utils.Database;
using DemoServer.Utils.Demos;
using DemoServer.Utils.Filters;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoServer.Controllers
{
    [Route("demo")]
    [ServiceFilter(typeof(AddUserIdToResponseHeaderAttribute))]
    public class HomeController : Controller
    {
        private readonly DemoContainer _demoContainer;
        private readonly UserIdContainer _userId;
        private readonly DatabaseLinks _databaseLinks;
        private readonly DatabaseSetup _databaseSetup;
        private readonly Settings _settings;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnv;

        public HomeController(DemoContainer demoContainer, UserIdContainer userId, DatabaseLinks databaseLinks,
            DatabaseSetup databaseSetup, Settings settings, ILogger<HomeController> logger, IHostingEnvironment hostingEnv)
        {
            _demoContainer = demoContainer;
            _userId = userId;
            _databaseSetup = databaseSetup;
            _databaseLinks = databaseLinks;
            _settings = settings;
            _logger = logger;
            _hostingEnv = hostingEnv;
        }

        private Guid UserId => _userId.Get();

        [HttpGet]
        [Route("get-context")]
        public IActionResult GetDemoContext()
        {
            var categoriesForLanguages = _demoContainer.GetCategoriesForLanguages();
            var categoriesWithVersions = _demoContainer.GetCategoriesWithVersions();

            var categoriesForLanguageDtos = categoriesForLanguages
                .Select(CategoriesForLanguageDto.FromModel)
                .ToList();

            var googleTagManagerContainerId = _hostingEnv.IsDevelopment()
                ? null
                : _settings.GoogleTagManager?.ContainerId;

            var dto = new DemoContextDto
            {
                CategoriesForLanguages = categoriesForLanguageDtos,
                CategoriesWithVersions = categoriesWithVersions,
                ConferenceMode = _settings.ConferenceMode,
                GoogleTagManagerContainerId = googleTagManagerContainerId
            };

            return Ok(dto);
        }

        [HttpGet]
        [Route("get/{language}/{categoryName}/{demoName}")]
        public IActionResult GetDemo(DemoLanguage language, string categoryName, string demoName)
        {
            try
            {
                var demo = _demoContainer.GetDemo(language, categoryName, demoName);
                var dto = DemoDto.FromModel(demo);

                dto.StudioUrl = GetStudioUrl(demo);

                return Ok(dto);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, "An error occured during demo fetching.");
                return NotFound();
            }
        }

        private string GetStudioUrl(Demo demo)
        {
            if (demo.StudioLinkToMediaDatabase)
                return _databaseLinks.ToMediaDocuments(UserId);

            if (demo.StudioLinkToIndexList)
                return _databaseLinks.ToUserIndexes(UserId);

            return _databaseLinks.ToUserDocuments(UserId);
        }

        [HttpPost]
        [Route("reset-database")]
        public async Task<IActionResult> ResetDatabase()
        {
            await _databaseSetup.ResetUserDatabase(UserId);
            await _databaseSetup.ResetMediaDatabase(UserId);

            return Ok("done");
        }
    }
}

using System;
using System.Linq;
using DemoServer.Models;
using DemoServer.Utils;
using DemoServer.Utils.Database;
using DemoServer.Utils.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoServer.Controllers
{
    [Route("demo")]
    [AddUserIdToHeader]
    public class HomeController : Controller
    {
        private readonly DemoContainer _demoContainer;
        private readonly HeadersAccessor _headersAccessor;
        private readonly DatabaseLinks _databaseLinks;
        private readonly ILogger _logger;

        public HomeController(DemoContainer demoContainer, HeadersAccessor headersAccessor, DatabaseLinks databaseLinks,
            ILogger<HomeController> logger)
        {
            _demoContainer = demoContainer;
            _headersAccessor = headersAccessor;
            _databaseLinks = databaseLinks;
            _logger = logger;
        }

        private Guid UserId => _headersAccessor.GetUserIdFromRequest();

        [HttpGet]
        [Route("get-versions")]
        public IActionResult GetDemoVersions()
        {
            var versions = _demoContainer.GetDemoVersions();
            var dtos = versions.Select(DemoVersionDto.FromEntry).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("get/{categoryName}/{demoName}")]
        public IActionResult GetDemo(string categoryName, string demoName)
        {
            try
            {
                var demo = _demoContainer.GetDemo(categoryName, demoName);
                var dto = DemoDto.FromModel(demo);
                dto.StudioUrl = _databaseLinks.ToDocuments(UserId);

                return Ok(dto);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, "An error occured during demo fetching.");
                return NotFound();
            }
        }
    }
}

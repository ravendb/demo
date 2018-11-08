using System;
using DemoServer.Models;
using DemoServer.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoServer.Controllers
{
    [Route("demo")]
    public class HomeController : Controller
    {
        private readonly DemoContainer _demoContainer;
        private readonly ILogger _logger;

        public HomeController(DemoContainer demoContainer, ILogger<HomeController> logger)
        {
            _demoContainer = demoContainer;
            _logger = logger;
        }

        [HttpGet]
        [Route("progress")]
        public IActionResult GetProgress()
        {
            //TODO
            //get user id from request
            //get progress from DB
            //create user id if needed

            var dto = new UserProgressDto();
            //dto.CompletedDemos.Add(new DemoProgressDto
            //{
            //    CategorySlug = "basics",
            //    DemoSlug = "demo101"
            //});
            return Ok(dto);
        }

        [HttpGet]
        [Route("get/{categoryName}/{demoName}")]
        public IActionResult GetDemo(string categoryName, string demoName)
        {
            try
            {
                var demo = _demoContainer.GetDemo(categoryName, demoName);
                var dto = DemoDto.FromModel(demo);
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

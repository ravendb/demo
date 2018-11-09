using System;
using System.Linq;
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

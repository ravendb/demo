using System;
using DemoServer.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoServer.Controllers
{
    [Route("demo")]
    public class DemoController : Controller
    {
        private readonly DemoContainer _demoContainer;
        private readonly ILogger _logger;

        public DemoController(DemoContainer demoContainer, ILogger<DemoController> logger)
        {
            _demoContainer = demoContainer;
            _logger = logger;
        }

        [HttpGet]
        [Route("get/{categoryName}/{demoName}")]
        public IActionResult GetDemo(string categoryName, string demoName)
        {
            try
            {
                var demo = _demoContainer.GetDemo(categoryName, demoName);
                return Ok(demo);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, "An error occured during demo fetching.");
                return NotFound();
            }
        }
    }
}

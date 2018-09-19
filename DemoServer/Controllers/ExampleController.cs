using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers
{
    [Route("example")]
    public class ExampleController
    {
        [HttpGet]
        [Route("get-data")]
        public ExampleDataDto GetData()
        {
            return new ExampleDataDto
            {
                Text = "example text"
            };
        }

        public class ExampleDataDto
        {
            public string Text { get; set; }
        }
    }
}

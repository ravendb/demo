using System.Threading.Tasks;
using DemoServer.Utils;
using Microsoft.AspNetCore.Mvc;
#region Usings
#endregion

namespace DemoServer.Controllers.Demos.Queries.SimpleQuery
{
    public class SimpleQueryDemoController : DemoCodeController
    {
        public SimpleQueryDemoController(HeadersAccessor headersAccessor) : base(headersAccessor)
        {
        }

        public override Task SetPrerequisites()
        {
            return Task.CompletedTask;
        }

        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            
            // TO BE IMPLEMENTED
            // I only added this demo to see how 2 categories will look like in main page

            #endregion region
            
            return Ok("Query OK");
        }

       
    }
}

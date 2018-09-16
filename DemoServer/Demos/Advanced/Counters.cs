using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/counters")]
        [Demo("Counters", DemoOutputType.String, demoOrder: 400)]
        public object Counters(int increment = 1)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session
                    .CountersFor("companies/1-A")
                    .Increment("Likes", increment);

                session.SaveChanges();
            }
            
            return $"Counter 'Likes' incremented successfully by '{increment}'";
        }
    }
}

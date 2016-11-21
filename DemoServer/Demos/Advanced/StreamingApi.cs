using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/streamingApi")]
        [Demo("Streaming Api", DemoOutputType.Flatten, demoOrder: 210)]
        public object StreamingApi()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var query = session.Query<Order>("OrderByCompanyAndCountry");

                int count = 0;

                using (var e = session.Advanced.Stream(query))
                {
                    while (e.MoveNext())
                    {
                        // do something with this
                        Order order = e.Current.Document;
                        count++;
                    }
                }

                return new { count };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : DemoApiController
    {
        [HttpGet]
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

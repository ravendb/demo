using System;
using System.Collections.Generic;
using System.Web.Http;
using DemoMethods.Entities;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object StreamingApi()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                // session.Query<Product>().ToList();
                // session.Query<Product>().Take(int.MaxValue).ToList();

                var query = session.Query<Order>("OrderByCompanyAndCountry");

                var e = session
                    .Advanced
                    .Stream(query);

                int count = 0;
                while (e.MoveNext())
                {
                    // do something with this
                    Order order = e.Current.Document;
                    count++;
                }

                return new { count };
            }
        }
    }
}

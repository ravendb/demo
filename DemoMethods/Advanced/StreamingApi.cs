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
                var query = session.Query<Product>("IndexManyProduct");

                var e = session
                    .Advanced
                    .Stream(query);

                var results = new List<Product>();

                while (e.MoveNext())
                {
                    results.Add(e.Current.Document);
                }

                return (results);
            }
        }
    }
}

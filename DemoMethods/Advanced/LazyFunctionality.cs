using System;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object LazyFunctionality()
        {
            //TODO: Load a company and its recent orders, total number of orders per the company, etc, first with several requests, then just one
            //
            //TODO: Query and facets using lazy

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Lazy<Product> result = session.Advanced.Lazily.Load<Product>("products/1");
                return result.Value;
            }
        }
    }
}

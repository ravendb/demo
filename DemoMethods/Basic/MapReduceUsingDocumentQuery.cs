using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoMethods.Helpers;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {
        [HttpGet]
        public object MapReduceUsingDocumentQuery()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                IList<ProductSales.Result> results = session
                    .Advanced
                    .DocumentQuery<ProductSales.Result, ProductSales>()
                    .ToList();

                return results;
            }
        }
    }
}
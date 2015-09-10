using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object MapReduceUsingDocumentQuery()
        {
            try
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
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        // TODO: Map reduce with linq API
        // TODO: Map reduce that uses a query

        [HttpGet]
        public object MapReduceUsingDocumenTQuery()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                IList<IndexProductSales.Result> results = session
                    .Advanced
                    .DocumentQuery<IndexProductSales.Result, IndexProductSales>()
                    .ToList();

                return results;
            }
        }
    }
}
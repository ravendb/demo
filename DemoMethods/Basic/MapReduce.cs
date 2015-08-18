using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object MapReduce()
        {
            new IndexProductSales().Execute(DocumentStoreHolder.Store);

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
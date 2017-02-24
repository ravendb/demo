using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/dynamicAggregation")]
        [Demo("Dynamic Aggregation", DemoOutputType.Flatten, demoOrder: 90)]
        public object DynamicAggregation(string fromVal = "0", string toVal = "999")
        {
            var from = int.Parse(fromVal);
            var to = int.Parse(toVal);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var result = session.Query<Product, Products>()
                    .Where(x => x.UnitsInStock >= from && x.UnitsInStock <= to)
                    .AggregateBy(x => x.Category)
                    .SumOn(x => x.UnitsInStock)
                    .ToList();

                return DemoUtilities.FormatRangeResults(result.Results);
            }
        }
    }
}

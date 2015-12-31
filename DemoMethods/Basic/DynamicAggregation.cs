using System;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Client;
using Raven.Client.Linq;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {
        [HttpGet]
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

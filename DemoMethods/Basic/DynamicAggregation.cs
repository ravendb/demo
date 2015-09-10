using System;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object DynamicAggregation(string fromVal = "10", string toVal = "20")
        {
            var from = int.Parse(fromVal);
            var to = int.Parse(toVal);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var result = session.Query<Product, Products>()
                    .AggregateBy(x => x.UnitsInStock)
                    .AddRanges(
                        x => x.UnitsInStock < from,
                        x => x.UnitsInStock >= from && x.UnitsInStock < to,
                        x => x.UnitsInStock >= to
                    )
                    .SumOn(x => x.UnitsInStock)
                    .ToList();

                return DemoUtilities.FormatRangeResults(result.Results);
            }
        }
    }
}

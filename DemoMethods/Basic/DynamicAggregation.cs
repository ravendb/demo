using System.Collections.Specialized;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {       
        [HttpGet]
        public object DynamicAggregation()
        {
            var userParams = new NameValueCollection
            {
                {"From", "10"},
                {"To", "20"}
            };
            DemoUtilities.GetUserParameters(Request.RequestUri.Query, userParams);

            var from = int.Parse(userParams["From"]);
            var to = int.Parse(userParams["To"]);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var result = session.Query<Product, IndexProducts>()
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

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;
using Raven.Client;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object LazyFunctionality()
        {
            var userParams = new NameValueCollection
            {
                {"HighPrice", "500"},
                {"DelayDays", "35"},
                {"From", "10"},
                {"To", "20"}
            };
            DemoUtilities.GetUserParameters(Request.RequestUri.Query, userParams);

            var highPrice = int.Parse(userParams["HighPrice"]);
            var delayDays = int.Parse(userParams["DelayDays"]);

            var from = decimal.Parse(userParams["From"]);
            var to = decimal.Parse(userParams["To"]);

            var facets = FacetRangeCreation.CreateFacets(from, to);

            //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var problematicOrders = session
                    .Query<IndexCostlyOrders.Result, IndexCostlyOrders>()
                    .Where(x => x.Price > highPrice && x.Delay > TimeSpan.FromDays(delayDays))
                    .OfType<Order>()
                    .Lazily();

                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = facets });
                session.SaveChanges();

                var facetResults = session
                    .Query<Product, IndexProductsAndPriceAndSuplier>()
                    .Where(x => x.UnitsInStock > 1)
                    .ToFacetsLazy(facets);

                Lazy<Company> company = session.Advanced.Lazily.Load<Company>("companies/1");
                Lazy<Product> product = session.Advanced.Lazily.Load<Product>("products/1");

                session.Advanced.Eagerly.ExecuteAllPendingLazyOperations();

                var fResults = DemoUtilities.FormatRangeResults(facetResults.Value.Results);

                var results = new
                {
                    problematicOrders,
                    company,
                    product,
                    fResults
                };
                return results;
            }
        }
    }
}

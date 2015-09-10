using System;
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
        public object LazyFunctionality(string highPriceVal = "500", string delayDaysVal = "35",
                                        string fromVal = "10", string toVal = "20")
        {
            var highPrice = int.Parse(highPriceVal);
            var delayDays = int.Parse(delayDaysVal);
            var from = decimal.Parse(fromVal);
            var to = decimal.Parse(toVal);

            var facets = FacetRangeCreation.CreateFacets(from, to);

            //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var problematicOrders = session
                    .Query<CostlyOrders.Result, CostlyOrders>()
                    .Where(x => x.Price > highPrice && x.Delay > TimeSpan.FromDays(delayDays))
                    .OfType<Order>()
                    .Lazily();

                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = facets });
                session.SaveChanges();

                var facetResults = session
                    .Query<Product, ProductsAndPriceAndSuplier>()
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

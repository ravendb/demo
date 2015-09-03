using System.Collections.Specialized;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object FacetsWithDocuments()
        {
            var userParams = new NameValueCollection
            {
                {"From", "10"},
                {"To", "20"}
            };
            DemoUtilities.GetUserParameters(Request.RequestUri.Query, userParams);

            var from = decimal.Parse(userParams["From"]);
            var to = decimal.Parse(userParams["To"]);

            var facets = FacetRangeCreation.CreateFacets(from, to);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = facets });
                session.SaveChanges();

                var facetResults = session
                    .Query<Product, IndexProductsAndPriceAndSuplier>()
                    .Where(x => x.UnitsInStock > 1)
                    .ToFacets(facets);

                return DemoUtilities.FormatRangeResults(facetResults.Results);
            }
        }
    }
}
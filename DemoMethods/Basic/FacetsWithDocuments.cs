using System;
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
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var facetResults = session
                    .Query<Product, ProductsAndPriceAndSuplier>()
                    .Where(x => x.UnitsInStock > 1)
                    .ToFacets(MenuController.FixedFacet);

                return DemoUtilities.FormatRangeResults(facetResults.Results);
            }
        }
    }
}
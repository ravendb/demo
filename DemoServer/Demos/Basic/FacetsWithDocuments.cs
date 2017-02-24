using System.Linq;
using DemoServer.Controllers;
using DemoServer.Demos.Menu;
using DemoServer.Entities;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/facetsWithDocuments")]
        [Demo("Facets With Documents", DemoOutputType.Flatten, demoOrder: 125)]
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
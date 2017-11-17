using System.Collections.Generic;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Queries.Facets;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/facetsDynamicRange")]
        [Demo("Facets Dynamic Range", DemoOutputType.Flatten, demoOrder: 130)]
        public object FacetsDynamicRange(string fromVal = "10", string toVal = "20")
        {
            var from = decimal.Parse(fromVal);
            var to = decimal.Parse(toVal);

            List<FacetBase> newFacet = new List<FacetBase>
            {
                new Facet
                {
                    FieldName = "Products"
                },
                new RangeFacet<Product>
                {
                    Ranges =
                    {
                        x => x.PricePerUnit < from,
                        x => x.PricePerUnit >= from && x.PricePerUnit < to,
                        x => x.PricePerUnit >= to,
                    }
                },
                new RangeFacet<Product>
                {
                    Ranges =
                    {
                        x => x.UnitsInStock < 10,
                        x => x.UnitsInStock >= 10
                    }
                }
            };

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var facetResults = session
                    .Query<Product, ProductsAndPriceAndSuplier>()
                    .Where(x => x.UnitsInStock > 1)
                    .AggregateBy(newFacet)
                    .Execute();

                return DemoUtilities.FormatRangeResults(facetResults);
            }
        }
    }
}
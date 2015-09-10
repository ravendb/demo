using System;
using System.Collections.Generic;
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
        public object FacetsDynamicRange(string fromVal = "10", string toVal = "20")
        {
            var from = decimal.Parse(fromVal);
            var to = decimal.Parse(toVal);

            List<Facet> newFacet = new List<Facet>()
            {
                new Facet
                {
                    Name = "Products"
                },
                new Facet<Product>
                {
                    Name = x => x.PricePerUnit,
                    Ranges =
                    {
                        x => x.PricePerUnit < from,
                        x => x.PricePerUnit > from && x.PricePerUnit < to,
                        x => x.PricePerUnit > to,
                    }
                },
                new Facet<Product>
                {
                    Name = x => x.UnitsInStock,
                    Ranges =
                    {
                        x => x.UnitsInStock < 10,
                        x => x.UnitsInStock > 10
                    }
                }
            };

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var facetResults = session
                    .Query<Product, ProductsAndPriceAndSuplier>()
                    .Where(x => x.UnitsInStock > 1)
                    .ToFacets(newFacet);

                return DemoUtilities.FormatRangeResults(facetResults.Results);
            }
        }
    }
}
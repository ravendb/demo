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
        private readonly List<Facet> facets = new List<Facet>
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
                    x => x.PricePerUnit < 10,
                    x => x.PricePerUnit > 10 && x.PricePerUnit < 20,
                    x => x.PricePerUnit > 20,
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

        [HttpGet]
        public object Facets()
        {
            Store.ExecuteIndex(new IndexProductsAndPriceAndSuplier());            

            using (var session = Store.OpenSession())
            {
                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = facets });
                session.SaveChanges();

                var facetResults = session
                    .Query<Product, IndexProductsAndPriceAndSuplier>()
                    .Customize(x => x.WaitForNonStaleResults())
                    .Where(x => x.UnitsInStock > 1)
                    .ToFacets(facets);                    
                    
                return facetResults;
                
            }
        }
    }
}
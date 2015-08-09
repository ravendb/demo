using DemoMethods.Entities;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        public class Index_ProductsAndPriceAndSuplier : AbstractIndexCreationTask<Product>
        {
            public class Result
            {
                public string ProductId { get; set; }
                public decimal Price { get; set; }
                public string Supplier { get; set; }
            }

            public Index_ProductsAndPriceAndSuplier()
            {
                Map = products => from product in products
                                  select new Result
                                  {
                                      ProductId = product.Id,
                                      Price = product.PricePerUnit,
                                      Supplier = product.Supplier
                                  };
            }

        }

        List<Facet> facets = new List<Facet>
        {
            new Facet
            {
                Name = "Products"
            },
            new Facet<Index_ProductsAndPriceAndSuplier.Result>
            {
                Name = x => x.Price,
                Ranges =
                {
                    x => x.Price < 10,
                    x => x.Price >= 10 && x.Price < 20 ,
                    x => x.Price > 20,
                }
            },
            //new Facet<Product>
            //{
            //    Name = x => x.Supplier,
            //    Ranges =
            //    {
            //        x => x.Supplier.StartsWith("M") == true,
            //        x => x.Supplier.StartsWith("M") == false
            //    }
            //}
        };

        [HttpGet]
        public object Facets()
        {
            Store.ExecuteIndex(new Index_ProductsAndPriceAndSuplier());            

            using (var session = Store.OpenSession())
            {
                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = facets });
                session.SaveChanges();

                var facetResults = session
                    .Query<Product, Index_ProductsAndPriceAndSuplier>()
                    .ToFacets(facets);
                return facetResults;
                
            }
        }
    }
}
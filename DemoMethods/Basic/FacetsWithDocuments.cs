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


        public class FacetsRangesResults
        {
            public string Key;
            public string Value;
            public int Count;
        }

        //TODO: Dynamic facets
        [HttpGet]
        public object FacetsWithDocuments()
        {
            /*
            var nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var setR1 = nvc["R1"] ?? "[NULL TO Dx10]";
            var pricePerUnitRange = new Facet<Product>();
            pricePerUnitRange.Name = Product.PricePerUnit.;
            foreach (var key in nvc)
            {
                if ( nvc[key] != null)

            }
             */

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                //TODO: Move to initialization
                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = facets });
                session.SaveChanges();

                var facetResults = session
                    .Query<Product, IndexProductsAndPriceAndSuplier>()
                    .Where(x => x.UnitsInStock > 1)
                    .ToFacets(facets);

                var result = facetResults.Results;

                
                List<FacetsRangesResults> lst = new List<FacetsRangesResults>();
                foreach (var key in result.Keys)
                {
                    for (int i = 0; i < result[key].Values.Count; i++)
                    {
                        var item = new FacetsRangesResults();
                        item.Key = key;
                        item.Value = result[key].Values[i].Range;
                        item.Count = result[key].Values[i].Count ?? 0;
                        lst.Add(item);
                    }
                }
                return lst;
            }
        }
    }
}
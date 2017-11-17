using System.Collections.Generic;
using DemoServer.Entities;
using Raven.Client.Documents.Queries.Facets;
using System.Linq;

namespace DemoServer.Indexes
{
    public class FacetRangeCreation
    {
        public static List<RangeFacet> CreateRangeFacets(decimal from, decimal to)
        {
            List<RangeFacet> facets = new List<RangeFacet>
            {
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
            return facets;
        }

        public static List<Facet> CreateFacets()
        {
            List<Facet> facets = new List<Facet>
            {
                new Facet
                {
                    FieldName = "Products"
                },
            };
            return facets;
        }


        public static List<FacetBase> CreateFacets(decimal from, decimal to)
        {
            return CreateRangeFacets(from, to)
                .Cast<FacetBase>()
                .Union(CreateFacets().Cast<FacetBase>())
                .ToList();
        }
    }

    public class FacetsRangesResults
    {
        public string Key;
        public string Value;
        public int Count;
    }
}
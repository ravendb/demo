using System.Collections.Generic;
using DemoMethods.Entities;
using Raven.Abstractions.Data;

namespace DemoMethods.Indexes
{
    public class FacetRangeCreation
    {
        public static List<Facet> CreateFacets(decimal from, decimal to)
        {
            List<Facet> facets = new List<Facet>
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
                        x => x.PricePerUnit >= from && x.PricePerUnit < to,
                        x => x.PricePerUnit >= to,
                    }
                },
                new Facet<Product>
                {
                    Name = x => x.UnitsInStock,
                    Ranges =
                    {
                        x => x.UnitsInStock < 10,
                        x => x.UnitsInStock >= 10
                    }
                }
            };
            return facets;
        }
    }

    public class FacetsRangesResults
    {
        public string Key;
        public string Value;
        public int Count;
    }
}
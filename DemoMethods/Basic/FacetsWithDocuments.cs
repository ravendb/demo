﻿using System;
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
        public object FacetsWithDocuments(string fromVal = "10", string toVal = "20")
        {
            try
            {
                var from = decimal.Parse(fromVal);
                var to = decimal.Parse(toVal);

                var facets = FacetRangeCreation.CreateFacets(from, to);

                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = facets });
                    session.SaveChanges();

                    var facetResults = session
                        .Query<Product, ProductsAndPriceAndSuplier>()
                        .Where(x => x.UnitsInStock > 1)
                        .ToFacets(facets);

                    return DemoUtilities.FormatRangeResults(facetResults.Results);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
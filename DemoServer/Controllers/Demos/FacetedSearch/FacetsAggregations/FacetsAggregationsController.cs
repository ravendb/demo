using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Queries.Facets;
#endregion

namespace DemoServer.Controllers.Demos.FacetedSearch.FacetsAggregations
{
    public class FacetsAggregationsController : DemoCodeController
    {
        public FacetsAggregationsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class Products_ByCategoryPriceAndUnits : AbstractIndexCreationTask<Product, Products_ByCategoryPriceAndUnits.IndexEntry>
        {
            public class IndexEntry
            {
                public string CategoryName { get; set; }
                public decimal PricePerUnit { get; set; }
                public int UnitsInStock { get; set; }
            }
            
            public Products_ByCategoryPriceAndUnits()
            {
                Map = products => from product in products
                    select new IndexEntry
                    {
                        CategoryName = LoadDocument<Category>(product.Category).Name,
                        PricePerUnit = product.PricePerUnit,
                        UnitsInStock = product.UnitsInStock
                    };
            }
        }
        #endregion
        #endregion
        
        protected override Task SetDemoPrerequisites() => new Products_ByCategoryPriceAndUnits().ExecuteAsync(DocumentStoreHolder.Store);
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            decimal range1 = runParams.Range1?? 25;
            decimal range2 = runParams.Range2?? 50;
            decimal range3 = runParams.Range3?? 100;

            #region Demo
            #region Step_2
            List<FacetBase> facets = new List<FacetBase>
            #endregion
            {
                #region Step_3
                new Facet
                {
                    FieldName = "CategoryName",
                    Aggregations =
                    {
                        {
                            FacetAggregation.Average,
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "PricePerUnit" } }
                        },
                        {
                            FacetAggregation.Sum,
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "UnitsInStock" } }
                        },
                        {
                            FacetAggregation.Max,
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "PricePerUnit" } }
                        },
                        {
                            FacetAggregation.Min,
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "PricePerUnit" }, new FacetAggregationField { Name = "UnitsInStock"} }
                        }
                    }
                },
                #endregion
                #region Step_4
                new RangeFacet<Product>
                {
                    Ranges =
                    {
                        product => product.PricePerUnit < range1,
                        product => product.PricePerUnit >= range1 && product.PricePerUnit < range2,
                        product => product.PricePerUnit >= range2 && product.PricePerUnit < range3,
                        product => product.PricePerUnit >= range3
                    },
                    Aggregations =
                    {
                        {
                            FacetAggregation.Average,
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "PricePerUnit" } }
                        },
                        {
                            FacetAggregation.Sum,
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "UnitsInStock" } }
                        },
                        {
                            FacetAggregation.Max, 
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "PricePerUnit" } }
                        },
                        {
                            FacetAggregation.Min,
                            new HashSet<FacetAggregationField> { new FacetAggregationField { Name = "PricePerUnit" }, new FacetAggregationField { Name = "UnitsInStock"} }
                        }
                    }
                }
                #endregion
            };

            Dictionary<string, FacetResult> queryResults;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                queryResults = session.Query<Product, Products_ByCategoryPriceAndUnits>()
                    .AggregateBy(facets)
                    .Execute();
                #endregion
            }
            #endregion
            return Ok(queryResults);
        }
        
        public class RunParams
        {
            public decimal? Range1 { get; set; }
            public decimal? Range2 { get; set; }
            public decimal? Range3 { get; set; }
        }
    }
}

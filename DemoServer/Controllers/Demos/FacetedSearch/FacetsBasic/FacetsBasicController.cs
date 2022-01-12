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

namespace DemoServer.Controllers.Demos.FacetedSearch.FacetsBasic
{
    public class FacetsBasicController : DemoCodeController
    {
        public FacetsBasicController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class Products_ByCategoryAndPrice : AbstractIndexCreationTask<Product, Products_ByCategoryAndPrice.IndexEntry>
        {
            public class IndexEntry
            {
                public string CategoryName { get; set; }
                public decimal PricePerUnit { get; set; }
            }
            
            public Products_ByCategoryAndPrice()
            {
                Map = products => from product in products
                    select new IndexEntry
                    {
                        CategoryName = LoadDocument<Category>(product.Category).Name,
                        PricePerUnit = product.PricePerUnit
                    };
            }
        }
        #endregion
        #endregion
        
        protected override Task SetDemoPrerequisites() => new Products_ByCategoryAndPrice().ExecuteAsync(DocumentStoreHolder.Store);
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int range1 = runParams.Range1?? 25;
            int range2 = runParams.Range2?? 50;
            int range3 = runParams.Range3?? 100;

            List<MyFacetResult> facetsResults = new List<MyFacetResult>();
            
            #region Demo
            #region Step_2
            List<FacetBase> facets = new List<FacetBase>
            #endregion
            {
                #region Step_3
                new Facet
                {
                    FieldName = "CategoryName",
                    DisplayFieldName = "Product Category"
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
                    DisplayFieldName = "Price per Unit"
                }
                #endregion
            };

            Dictionary<string, FacetResult> queryResults;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                var query = session.Query<Product, Products_ByCategoryAndPrice>()
                #endregion
                    #region Step_6
                    .AggregateBy(facets);
                    #endregion
                    
                #region Step_7
                queryResults = query.Execute();
                #endregion
            }
            #endregion

            foreach (var result in queryResults)
            {
                var facetName = result.Value.Name;

                foreach (var item in result.Value.Values)
                {
                    facetsResults.Add(new MyFacetResult()
                    {
                        FacetName = facetName, // i.e. PricePerUnit
                        FacetRange = item.Range, // i.e. PricePerUnit < 50
                        FacetCount = item.Count 
                    });
                }
            }

            return Ok(facetsResults);
        }
        
        public class RunParams
        {
            public int? Range1 { get; set; }
            public int? Range2 { get; set; }
            public int? Range3 { get; set; }
        }

        private class MyFacetResult
        {
            public string FacetName { get; set; }
            public string FacetRange { get; set; }
            public decimal FacetCount { get; set; }
        }
    }
}

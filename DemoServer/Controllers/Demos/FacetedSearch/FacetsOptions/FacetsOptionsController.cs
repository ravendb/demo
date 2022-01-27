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

namespace DemoServer.Controllers.Demos.FacetedSearch.FacetsOptions
{
    public class FacetsOptionsController : DemoCodeController
    {
        public FacetsOptionsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class Products_ByCategoryAndSupplier : AbstractIndexCreationTask<Product, Products_ByCategoryAndSupplier.IndexEntry>
        {
            public class IndexEntry
            {
                public string CategoryName { get; set; }
                public string Supplier { get; set; }
            }
            
            public Products_ByCategoryAndSupplier()
            {
                Map = products => from product in products
                    select new IndexEntry
                    {
                        CategoryName = LoadDocument<Category>(product.Category).Name,
                        Supplier = product.Supplier
                    };
            }
        }
        #endregion
        #endregion
        
        protected override Task SetDemoPrerequisites() => new Products_ByCategoryAndSupplier().ExecuteAsync(DocumentStoreHolder.Store);
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int start = runParams.Start?? 3;
            int pageSize = runParams.PageSize?? 2;

            string include = runParams.IncludeRemainingTerms;
            bool includeRemainingTerms = include == null || include == "true";
            
            #region Demo
            #region Step_2
            List<FacetBase> facets = new List<FacetBase>
            #endregion
                {
                    #region Step_3
                    new Facet
                    {
                        FieldName = "CategoryName",
                        Options = new FacetOptions()
                        {
                            Start = start,
                            PageSize = pageSize,
                            IncludeRemainingTerms = includeRemainingTerms,
                            TermSortMode = FacetTermSortMode.CountDesc
                        },
                    },
                    #endregion
                    new Facet
                    {
                        FieldName = "Supplier",
                        Options = new FacetOptions()
                        {
                            Start = start,
                            PageSize = pageSize,
                            IncludeRemainingTerms = includeRemainingTerms,
                            TermSortMode = FacetTermSortMode.ValueAsc
                        }
                    }
                };

            Dictionary<string, FacetResult> queryResults;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_4
                queryResults = session.Query<Product, Products_ByCategoryAndSupplier>()
                    .AggregateBy(facets)
                    .Execute();
                #endregion
            }
            #endregion
            
            return Ok(queryResults);
        }

        public class RunParams
        {
            public int? Start { get; set; }
            public int? PageSize { get; set; }
            public string IncludeRemainingTerms { get; set; }
        }
    }
}

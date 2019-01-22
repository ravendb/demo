using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.RelatedDocuments.IndexRelatedDocuments
{
    public class IndexRelatedDocumentsController : DemoCodeController
    {
        public IndexRelatedDocumentsController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Products_ByCategoryName : AbstractIndexCreationTask<Product>
            #endregion
        {
            #region Step_2
            public class Result
            {
                public string CategoryName { get; set; }
            }
            #endregion
            
            #region Step_3
            public Products_ByCategoryName()
            {
                Map = products => from product in products
                    select new
                    {
                        CategoryName = LoadDocument<Category>(product.Category).Name
                    };
            }
            #endregion
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Products_ByCategoryName().ExecuteAsync(DocumentStoreHolder.Store);
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string categoryName = runParams.CategoryName;
            
            #region Demo
            List<Product> productsWithCategoryName;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_4
                productsWithCategoryName = session
                    .Query<Products_ByCategoryName.Result, Products_ByCategoryName>()
                    .Where(x => x.CategoryName == categoryName)
                    .OfType<Product>()
                    .ToList();
                #endregion
            }
            #endregion
            
            return Ok(productsWithCategoryName);
        }

        public class RunParams
        {
            public string CategoryName { get; set; }
        }
    }
}

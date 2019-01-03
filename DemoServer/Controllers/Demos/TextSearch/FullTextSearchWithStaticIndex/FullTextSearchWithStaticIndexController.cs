using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.FullTextSearchWithStaticIndex
{
    public class FullTextSearchWithStaticIndexController : DemoCodeController
    {
        public FullTextSearchWithStaticIndexController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
        }
        #region Demo
        
        #region Step_1
        public class Categories_DescriptionText : AbstractIndexCreationTask<Category, Categories_DescriptionText.Result>
            #endregion
        {
            #region Step_2
            public class Result
            {
                public string CategoryDescription { get; set; }
            }
            #endregion
           
            public Categories_DescriptionText()
            {
                #region Step_3
                Map = categories => from category in categories
                    select new Result
                    {
                        CategoryDescription = category.Description
                    };
                #endregion
                
                #region Step_4
                Indexes.Add(x => x.CategoryDescription, FieldIndexing.Search);
                #endregion
            }
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var someFood = runParams.SomeFood;
            
            new Categories_DescriptionText().Execute(DocumentStoreHolder.Store);
         
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                var categoriesWithSomeFood = session.Query<Categories_DescriptionText.Result, Categories_DescriptionText>()
                       .Where(x => x.CategoryDescription == someFood)
                       .OfType<Category>()
                       .ToList();
                #endregion
            }
            
            //TODO 1: How to show results ? 
            //TODO 2: Split the demo region to 2 parts, so that we have more control over what is shown !
            return Ok($"Query results are: ... TODO: Show Query Results ..."); 
        }
        #endregion
        
        public class RunParams
        {
            public string SomeFood { get; set; }
        }
    }
}

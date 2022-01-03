﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
 #region Usings
using System.Linq;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.FTSWithStaticIndexSingleField
{
    public class FTSWithStaticIndexSingleFieldController : DemoCodeController
    {
        public FTSWithStaticIndexSingleFieldController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class Categories_DescriptionText : AbstractIndexCreationTask<Category, Categories_DescriptionText.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string CategoryDescription { get; set; }
            }
            #endregion
           
            public Categories_DescriptionText()
            {
                #region Step_3
                Map = categories => from category in categories
                    select new IndexEntry
                    {
                        CategoryDescription = category.Description
                    };
                #endregion
                
                #region Step_4
                Index(x => x.CategoryDescription, FieldIndexing.Search);
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Categories_DescriptionText().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string searchTerm = runParams.SearchTerm?? "Pasta";

            #region Demo
            List<Category> categoriesWithSearchTerm;
         
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                categoriesWithSearchTerm = session.Query<Categories_DescriptionText.IndexEntry, Categories_DescriptionText>()
                       .Where(x => x.CategoryDescription == searchTerm)
                       .OfType<Category>()
                       .ToList();
                #endregion
            }
            #endregion

            return Ok(categoriesWithSearchTerm); 
        }
        
        public class RunParams
        {
            public string SearchTerm { get; set; }
        }
    }
}

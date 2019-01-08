﻿using System.Collections.Generic;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
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
        public FTSWithStaticIndexSingleFieldController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
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
            string searchTerm = runParams.SearchTerm;
            List<Category> categoriesWithSearchTerm;
                
            new Categories_DescriptionText().Execute(DocumentStoreHolder.Store);
         
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                categoriesWithSearchTerm = session.Query<Categories_DescriptionText.Result, Categories_DescriptionText>()
                       .Where(x => x.CategoryDescription == searchTerm)
                       .OfType<Category>()
                       .ToList();
                #endregion
            }
            
            return Ok(categoriesWithSearchTerm); 
        }
        #endregion
        
        public class RunParams
        {
            public string SearchTerm { get; set; }
        }
    }
}

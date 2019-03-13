using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.PagingQueryResults
{
    public class PagingQueryResultsController : DemoCodeController
    {
        public PagingQueryResultsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int resultsToSkip = runParams.ResultsToSkip;
            int resultsToTake = runParams.ResultsToTake;

            #region Demo
            List<Company> pagedResults;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                pagedResults = session.Query<Company>()
                #endregion
                     #region Step_2
                     .Statistics(out QueryStatistics stats) 
                     #endregion
                     #region Step_3
                     .Skip(resultsToSkip)
                     #endregion
                     #region Step_4
                     .Take(resultsToTake)
                     #endregion
                     #region Step_5
                     .ToList();
                     #endregion
                     
                     #region Step_6
                     int totalResults = stats.TotalResults;
                     #endregion
            }
            #endregion 
            
            return Ok(pagedResults);
        }
        
        public class RunParams
        {
            public int ResultsToSkip { get; set; }
            public int ResultsToTake { get; set; }
        }
    }
}

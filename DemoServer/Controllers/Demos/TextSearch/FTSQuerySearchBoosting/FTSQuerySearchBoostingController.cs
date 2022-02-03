using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.FTSQuerySearchBoosting
{
    public class FTSQuerySearchBoostingController : DemoCodeController
    {
        public FTSQuerySearchBoostingController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int boost1 = runParams.Boost1?? 100;
            int boost2 = runParams.Boost2?? 20;
            int boost3 = runParams.Boost3?? 5;
            
            #region Demo
            List<Employee> employeesWithMatchingTerms;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                employeesWithMatchingTerms = session.Query<Employee>()
                    #endregion
                    #region Step_2
                    .Search(x => x.Notes, "ph.d.", boost: boost1)
                    .Search(x => x.Notes, "university", boost: boost2)
                    .Search(x => x.Notes, "college", boost: boost3)
                    #endregion
                    #region Step_3
                    .ToList();
                    #endregion
            }
            #endregion 
            
            return Ok(employeesWithMatchingTerms);
        }
        
        public class RunParams
        {
            public int? Boost1 { get; set; }
            public int? Boost2 { get; set; }
            public int? Boost3 { get; set; }
        }
    }
}

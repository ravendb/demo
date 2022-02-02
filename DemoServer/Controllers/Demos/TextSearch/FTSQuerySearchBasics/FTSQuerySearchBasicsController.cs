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

namespace DemoServer.Controllers.Demos.TextSearch.FTSQuerySearchBasics
{
    public class FTSQuerySearchBasicsController : DemoCodeController
    {
        public FTSQuerySearchBasicsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string term1 = runParams.Term1?? "Washington";
            string term2 = runParams.Term2?? "Colorado";
            
            #region Demo
            List<Employee> employeesWithMatchingTerms;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                employeesWithMatchingTerms = session.Query<Employee>()
                    #endregion
                    #region Step_2
                    .Search(x => x.Notes, $"{term1} {term2}")
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
            public string Term1 { get; set; }
            public string Term2 { get; set; }
        }
    }
}

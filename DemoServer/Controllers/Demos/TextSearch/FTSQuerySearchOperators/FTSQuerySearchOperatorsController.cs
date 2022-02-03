using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents;
using System.Collections.Generic;
using Raven.Client.Documents.Queries;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.FTSQuerySearchOperators
{
    public class FTSQuerySearchOperatorsController : DemoCodeController
    {
        public FTSQuerySearchOperatorsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string term1 = runParams.Term1?? "Spanish";
            string term2 = runParams.Term2?? "Portuguese";
            string term3 = runParams.Term3?? "Manager";
            
            #region Demo
            List<Employee> employeesWithMatchingTerms;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                employeesWithMatchingTerms = session.Query<Employee>()
                    #endregion
                    #region Step_2
                    .Search(x => x.Notes, $"{term1} {term2}", @operator: SearchOperator.And)
                    #endregion
                    #region Step_3
                    .Search(x => x.Notes, $"{term3}", options: SearchOptions.Or)
                    #endregion
                    #region Step_4
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
            public string Term3 { get; set; }
        }
    }
}

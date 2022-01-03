using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.AutoIndexes.AutoMapIndex1
{
    public class AutoMapIndex1Controller : DemoCodeController
    {
        public AutoMapIndex1Controller(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string firstName = runParams.FirstName?? "Steven";

            #region Demo
            Employee employeeResult;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<Employee> findEmployeeQuery = session.Query<Employee>()
                    .Where(x => x.FirstName == firstName);
                #endregion
                
                #region Step_2
                employeeResult = findEmployeeQuery.FirstOrDefault();
                #endregion
            }
            #endregion 
            
            return Ok(employeeResult);
        }
        
        public class RunParams
        {
            public string FirstName { get; set; }
        }
    }
}

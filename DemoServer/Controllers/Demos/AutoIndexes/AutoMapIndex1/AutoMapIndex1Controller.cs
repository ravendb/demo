using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
#endregion

namespace DemoServer.Controllers.Demos.AutoIndexes.AutoMapIndex1
{
    public class AutoMapIndex1Controller : DemoCodeController
    {
        public AutoMapIndex1Controller(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var firstName = runParams.FirstName;

            #region Demo
            Employee employeeResult;
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
               
            {
                #region Step_1
                var findEmployeeQuery = session.Query<Employee>()
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

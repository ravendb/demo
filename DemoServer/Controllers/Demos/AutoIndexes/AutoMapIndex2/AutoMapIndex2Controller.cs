using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.AutoIndexes.AutoMapIndex2
{
    public class AutoMapIndex2Controller : DemoCodeController
    {
        public AutoMapIndex2Controller(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache, 
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var country = runParams.Country;

            #region Demo
            Employee employeeResult;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<Employee> findEmployeeQuery = session.Query<Employee>()
                    .Where(x => x.Address.Country == country &&
                                x.Title.StartsWith("Sales"));
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
            public string Country { get; set; }
        }
    }
}

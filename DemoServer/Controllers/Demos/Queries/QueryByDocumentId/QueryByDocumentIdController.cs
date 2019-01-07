using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.QueryByDocumentId
{
    public class QueryByDocumentIdController : DemoCodeController
    {
        public QueryByDocumentIdController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string employeeDocumentId = runParams.EmployeeDocumentId;

            #region Demo
            Employee employee;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<Employee> queryByDocumentId = session.Query<Employee>()
                #endregion
                #region Step_2
                      .Where(x => x.Id == employeeDocumentId);
                #endregion
                
                #region Step_3
                employee = queryByDocumentId.FirstOrDefault();
                #endregion
            }
            #endregion 
            
            return Ok(employee);
        }
        
        public class RunParams
        {
            public string EmployeeDocumentId { get; set; }
        }
    }
}

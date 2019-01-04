using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.QueryByDocumentId
{
    public class QueryByDocumentIdController : DemoCodeController
    {
        public QueryByDocumentIdController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, databaseSetup)
        {
        }

        private Employee initialCompanyDocument => new Employee
        {
            Id = "employees/1-A",
            FirstName = "Ayende",
            LastName = "Rahien",
            HomePhone = "(+972)52-5486969"
        };
        
        private async Task SetRunPrerequisites(string employeeDocumentId)
        {
            await DatabaseSetup.EnsureUserDocumentExists(UserId, employeeDocumentId, initialCompanyDocument);
        }
        
        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var employeeDocumentId = runParams.EmployeeDocumentId;
            await SetRunPrerequisites(employeeDocumentId);
            
            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var queryByDocumentId = session.Query<Employee>()
                #endregion
                #region Step_2
                      .Where(x => x.Id == employeeDocumentId);
                #endregion
                
                #region Step_3
                var employee = queryByDocumentId.FirstOrDefault();
                #endregion
            }
            
            #endregion 
            
            //TODO: How to show results ?
            return Ok($"Document {employeeDocumentId} details are: ... TODO: Show Query Results ..."); 
        }
        
        public class RunParams
        {
            public string EmployeeDocumentId { get; set; }
        }
    }
}

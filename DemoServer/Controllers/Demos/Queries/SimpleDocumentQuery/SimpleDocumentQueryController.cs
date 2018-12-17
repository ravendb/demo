using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.SimpleDocumentQuery
{
    public class SimpleDocumentQueryController : DemoCodeController
    {
        public SimpleDocumentQueryController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
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
            await DatabaseSetup.EnsureDocumentExists(UserId, employeeDocumentId, initialCompanyDocument);
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
                var documentQuery = session.Query<Employee>().Where(x => x.Id == employeeDocumentId);
                var employee = documentQuery.FirstOrDefault();
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

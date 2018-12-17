using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.EditDocument
{
    public class EditDocumentController : DemoCodeController
    {
        private const string DocumentId = "companies/1-A"; 

        public EditDocumentController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
        }

        private Company InitialCompany => new Company
        {
            Id = DocumentId,
            Name = "Company Name",
            Phone = "(+972)52-5486969"
        };

        private async Task SetRunPrerequisites()
        {
            await DatabaseSetup.EnsureDocumentExists(UserId, DocumentId, InitialCompany);
        }

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var companyName = runParams.CompanyName;

            await SetRunPrerequisites();
            
            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var company = session.Load<Company>("companies/1-A");
                #endregion
                
                #region Step_2
                company.Name = companyName;
                #endregion
                
                #region Step_3
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"Document {DocumentId} was edited successfully");
        }

        public class RunParams
        {
            public string CompanyName { get; set; }
        }
    }
}

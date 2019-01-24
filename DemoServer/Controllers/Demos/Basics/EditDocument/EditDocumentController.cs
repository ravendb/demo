using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Basics.EditDocument
{
    public class EditDocumentController : DemoCodeController
    {
        private const string DocumentId = "companies/5-A"; 

        public EditDocumentController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
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
            await DatabaseSetup.EnsureUserDocumentExists(UserId, DocumentId, InitialCompany);
        }
        
        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            string companyName = runParams.CompanyName;
            
            await SetRunPrerequisites();

            #region Demo
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                Company company = session.Load<Company>("companies/5-A");
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

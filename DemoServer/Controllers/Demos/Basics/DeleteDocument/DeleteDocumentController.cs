using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Basics.DeleteDocument
{
    public class DeleteDocumentController : DemoCodeController
    {
        public DeleteDocumentController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        private Company initialCompanyDocument => new Company
        {
            Id = "companies/1-A",
            Name = "Company Name",
            Phone = "(+972)52-5486969"
        };


        private async Task SetRunPrerequisites(string documentId)
        {
            await DatabaseSetup.EnsureUserDocumentExists(UserId, documentId, initialCompanyDocument);
        }
        
        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            string documentId = runParams.DocumentId;
            initialCompanyDocument.Id = documentId;
            
            await SetRunPrerequisites(documentId);

            #region Demo
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                session.Delete(documentId);
                #endregion
                
                #region Step_2
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"Document {documentId} was deleted successfully");
        }

        public class RunParams
        {
            public string DocumentId { get; set; }
        }
    }
}

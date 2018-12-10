using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.DeleteDocument
{
    public class DeleteDocumentController : DemoCodeController
    {
        public DeleteDocumentController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }

        private Company initialCompanyDocument => new Company
        {
            Id = "companies/1-A",
            Name = "Company Name",
            Phone = "(+972)52-5486969"
        };

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var documentId = runParams.documentID;
            initialCompanyDocument.Id = documentId;
            
            // Verify document exists here (and not in SetDemoPrerequisites) since:
            //    demo can be run multiple times -or-
            //    document to be deleted can come from demo parameters
            
            await DatabaseAccessor.EnsureDocumentExists(UserId, documentId, initialCompanyDocument);

            #region Demo
            
            #region Step_1
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            {
                #region Step_2
                session.Delete(documentId);
                #endregion
                
                #region Step_3
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"Document {documentId} was deleted successfully");
        }

        private class Company
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
        }

        public class RunParams
        {
            public string documentID { get; set; }
        }
    }
}

using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
#endregion

namespace DemoServer.Controllers.Demos.Basics.DeleteDocument
{
    public class DeleteDocumentController : DemoCodeController
    {
        public DeleteDocumentController(HeadersAccessor headersAccessor, DatabaseAccessor databaseAccessor) : base(
            headersAccessor, databaseAccessor)
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
            var serverUrl = DatabaseAccessor.GetFirstDatabaseUrl();
            var databaseName = DatabaseAccessor.GetDatabaseName(UserId);

            var documentID = runParams.documentID;
            initialCompanyDocument.Id = documentID;
            
            // Verify document exists here (and not in SetDemoPrerequisites) since:
            //    demo can be run multiple times -or-
            //    document to be deleted can come from demo parameters
            
            await DatabaseAccessor.EnsureDocumentExists(UserId, documentID, initialCompanyDocument);

            #region Demo
            
            #region Step_1
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverUrl }, 
                Database = databaseName
            };
            
            documentStore.Initialize();
            #endregion

            #region Step_2
            // Open the session for work
            using (var session = documentStore.OpenSession())
            #endregion
            {
                #region Step_3
                // Mark the entity to be deleted in the session
                session.Delete(documentID);
                #endregion
                
                #region Step_4
                // Document is deleted upon saving the changes
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"Document {documentID} was deleted successfully");
        }

        public class Company
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

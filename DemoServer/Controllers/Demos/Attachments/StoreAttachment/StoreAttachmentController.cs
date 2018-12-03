using System;
using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
using System.IO;
#endregion

namespace DemoServer.Controllers.Demos.Attachments.StoreAttachment
{
    public class StoreAttachmentController : DemoCodeController
    {
        public StoreAttachmentController(HeadersAccessor headersAccessor, DatabaseAccessor databaseAccessor) : base(
            headersAccessor, databaseAccessor)
        {
        }

        private const string DemoPath = @".\demo"; // TODO - which directory should be used ?
        
        private Company initialCompanyDocument => new Company
        {
            Id = "companies/2-A",
            Name = "Company Name",
            Address = "1 Plaza Center"
        };

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var serverUrl = DatabaseAccessor.GetFirstDatabaseUrl();
            var databaseName = DatabaseAccessor.GetDatabaseName(UserId);

            var documentId = runParams.DocumentId;
            initialCompanyDocument.Id = documentId;
            
            var attachment1 = runParams.Attachment1;
            var attachment2 = runParams.Attachment2;
            var attachmentType1 = runParams.AttachmentType1;
            var attachmentType2 = runParams.AttachmentType2;
            
            // Verify here (and not in SetDemoPrerequisites) since demo can be run multiple times with diff params
            await DatabaseAccessor.EnsureDocumentExists(UserId, documentId, initialCompanyDocument);

            DatabaseAccessor.EnsureFileExists(DemoPath, attachment1, 100);
            DatabaseAccessor.EnsureFileExists(DemoPath, attachment2, 200);

            #region Demo
            
            #region Step_1
            // Init the Document Store
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverUrl }, 
                Database = databaseName
            };
            
            documentStore.Initialize();
            #endregion
          
            // Set the demo file path 
            string attachmentFile1 = Path.Combine(DemoPath, attachment1); 
            string attachmentFile2 = Path.Combine(DemoPath, attachment2);
          
            #region Step_2
            // Open the session for work
            using (var session = documentStore.OpenSession())
            #endregion
            
            #region Step_3 
            // Open binary stream for the attachments files
            using (var fileStream1 = System.IO.File.Open(attachmentFile1, FileMode.Open))
            using (var fileStream2 = System.IO.File.Open(attachmentFile2, FileMode.Open))
            #endregion
            {
                #region Step_4
                // Attach the files to the document
                session.Advanced.Attachments.Store(documentId, attachmentFile1, fileStream1, attachmentType1);
                session.Advanced.Attachments.Store(documentId, attachmentFile2, fileStream2, attachmentType2);
                #endregion 
                
                #region Step_5
                // Save the changes
                session.SaveChanges();
                #endregion
            }
            #endregion
          
            //TODO: Should we delete these files that were created in beginning of this demo ?
            //      Because user can run this demo multiple times with different files from the parameters each time...
            
            return Ok($"Attachments {attachment1} & {attachment2} were stored successfully on document {documentId}");
        }
    }
    
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    
    public class RunParams
    {
        public string DocumentId { get; set; }
        public string Attachment1 { get; set; }
        public string Attachment2 { get; set; }
        public string AttachmentType1 { get; set; }
        public string AttachmentType2 { get; set; }
    }
}

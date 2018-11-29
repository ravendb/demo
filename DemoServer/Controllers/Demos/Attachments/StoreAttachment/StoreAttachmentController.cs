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

        private const string DemoPath = @"c:\demo\"; // TODO - use current directory instead ? 
        
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

            var documentID = runParams.documentID;
            initialCompanyDocument.Id = documentID;
            
            var attachment1 = runParams.attachment1;
            var attachment2 = runParams.attachment2;
            var attachmentType1 = runParams.attachmentType1;
            var attachmentType2 = runParams.attachmentType2;
            
            // Verify here (and not in SetDemoPrerequisites) since demo can be run multiple times with diff params
            await DatabaseAccessor.EnsureDocumentExists(UserId, documentID, initialCompanyDocument);
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
                session.Advanced.Attachments.Store(documentID, attachmentFile1, fileStream1, attachmentType1);
                session.Advanced.Attachments.Store(documentID, attachmentFile2, fileStream2, attachmentType2);
                #endregion 
                
                #region Step_5
                // Save the changes
                session.SaveChanges();
                #endregion
            }
            #endregion
           
            return Ok($"Attachments {attachment1} & {attachment2} were stored successfully on document {documentID}");
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
        public string documentID { get; set; }
        public string attachment1 { get; set; }
        public string attachment2 { get; set; }
        public string attachmentType1 { get; set; }
        public string attachmentType2 { get; set; }
    }
}

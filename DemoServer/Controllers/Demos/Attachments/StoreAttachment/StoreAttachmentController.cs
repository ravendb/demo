using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.IO;
#endregion

namespace DemoServer.Controllers.Demos.Attachments.StoreAttachment
{
    public class StoreAttachmentController : DemoCodeController
    {
        public StoreAttachmentController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }

        private const string DemoPath = @".\demo"; // TODO - which directory should be used ?
        
        private Company initialCompanyDocument => new Company
        {
            Id = "companies/2-A",
            Name = "Company Name",
            Address = "1 Plaza Center"
        };

        private async Task SetRunPrerequisites(string documentId, string attachment1, string attachment2)
        {
            await DatabaseAccessor.EnsureDocumentExists(UserId, documentId, initialCompanyDocument);

            DatabaseAccessor.EnsureFileExists(DemoPath, attachment1, 100);
            DatabaseAccessor.EnsureFileExists(DemoPath, attachment2, 200);
         }
        
        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var documentId = runParams.DocumentId;
            initialCompanyDocument.Id = documentId;
            
            var attachment1 = runParams.Attachment1;
            var attachment2 = runParams.Attachment2;
            var attachmentType1 = runParams.AttachmentType1;
            var attachmentType2 = runParams.AttachmentType2;
            
           await SetRunPrerequisites(documentId, attachment1, attachment2);

            #region Demo
            
            string attachmentFile1 = Path.Combine(DemoPath, attachment1); 
            string attachmentFile2 = Path.Combine(DemoPath, attachment2);
          
            #region Step_1
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            
            #region Step_2 
            using (var fileStream1 = System.IO.File.Open(attachmentFile1, FileMode.Open))
            using (var fileStream2 = System.IO.File.Open(attachmentFile2, FileMode.Open))
            #endregion
            {
                #region Step_3
                session.Advanced.Attachments.Store(documentId, attachmentFile1, fileStream1, attachmentType1);
                session.Advanced.Attachments.Store(documentId, attachmentFile2, fileStream2, attachmentType2);
                #endregion 
                
                #region Step_4
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

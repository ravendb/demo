using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Http;
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

        private Company initialCompanyDocument => new Company
        {
            Id = "companies/2-A",
            Name = "Company Name",
            Address = "1 Plaza Center"
        };

        private async Task SetRunPrerequisites(string documentId, IFormFile attachment1, IFormFile attachment2)
        {
            await DatabaseAccessor.EnsureDocumentExists(UserId, documentId, initialCompanyDocument);

            //TODO: rethink the demo default parameters
            //DatabaseAccessor.EnsureFileExists(DemoPath, attachment1, 100);
            //DatabaseAccessor.EnsureFileExists(DemoPath, attachment2, 200);
         }
        
        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var documentId = runParams.DocumentId;
            initialCompanyDocument.Id = documentId;
            
            var attachment1 = runParams.Attachment1;
            var attachment2 = runParams.Attachment2;
            var attachmentName1 = attachment1.Name;
            var attachmentName2 = attachment2.Name;
            var attachmentType1 = runParams.AttachmentType1;
            var attachmentType2 = runParams.AttachmentType2;

            await SetRunPrerequisites(documentId, attachment1, attachment2);

            #region Demo
            
            #region Step_1
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            #region Step_2 
            using (var stream1 = attachment1.OpenReadStream())
            using (var stream2 = attachment2.OpenReadStream())
            #endregion
            {
                #region Step_3
                session.Advanced.Attachments.Store(documentId, attachmentName1, stream1, attachmentType1);
                session.Advanced.Attachments.Store(documentId, attachmentName2, stream2, attachmentType2);
                #endregion 
                
                #region Step_4
                session.SaveChanges();
                #endregion
            }
            #endregion
          
            //TODO: Should we delete these files that were created in beginning of this demo ?
            //      Because user can run this demo multiple times with different files from the parameters each time...
            
            return Ok($"Attachments {attachmentName1} & {attachmentName2} were stored successfully on document {documentId}");
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
        public IFormFile Attachment1 { get; set; }
        public IFormFile Attachment2 { get; set; }
        public string AttachmentType1 { get; set; }
        public string AttachmentType2 { get; set; }
    }
}

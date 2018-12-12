using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Attachments;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Attachments.StoreAttachment
{
    public class StoreAttachmentController : DemoCodeController
    {
        private const string DefaultDocumentId = "companies/2-A";
        private const string DefaultFilePath = ".//DemoResources//raven_logo.png";

        public StoreAttachmentController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }

        private Company InitialCompanyDocument => new Company
        {
            Id = DefaultDocumentId,
            Name = "Company Name",
            Address = "1 Plaza Center"
        };

        protected override async Task SetDemoPrerequisites()
        {
            await DatabaseAccessor.EnsureDocumentExists(UserId, DefaultDocumentId, InitialCompanyDocument);
        }

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var documentId = runParams.DocumentId ?? DefaultDocumentId;

            await SetRunPrerequisites(documentId);

            var attachmentName = runParams.AttachmentName;
            var contentType = runParams.ContentType;
            AttachmentWrapper attachment;

            if (runParams.Attachment == null)
            {
                attachment = new LocalFileAttachmentWrapper(DefaultFilePath);
            }
            else
            {
                FileValidator.Check(runParams.Attachment);
                attachment = new FormFileAttachmentWrapper(runParams.Attachment);
            }

            #region Demo

            using (var session = DocumentStoreHolder.Store.OpenSession())
            #region Step_1 
            using (var stream = attachment.OpenStream())
            #endregion
            {
                #region Step_2
                session.Advanced.Attachments.Store(documentId, attachmentName, stream, contentType);
                #endregion 
                
                #region Step_3
                session.SaveChanges();
                #endregion
            }
            #endregion
          
            return Ok($"Attachment {attachmentName} was stored successfully on document {documentId}");
        }

        private async Task SetRunPrerequisites(string documentId)
        {
            await DatabaseAccessor.EnsureDocumentExists(UserId, documentId, InitialCompanyDocument);
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
        public IFormFile Attachment { get; set; }
        public string AttachmentName { get; set; }
        public string ContentType { get; set; }
    }
}

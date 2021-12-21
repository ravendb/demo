using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.IO;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Attachments.LoadAttachment
{
    public class LoadAttachmentController : DemoCodeController
    {
        private const string DefaultDocumentId = "categories/1-A";

        public LoadAttachmentController(UserIdContainer userId, UserStoreCache userStoreCache,
            MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        private Category InitialCategoryDocument => new Category
        {
            Id = DefaultDocumentId,
            Name = "Category Name",
            Description = "Category Description"
        };

        private async Task SetRunPrerequisites(string documentId)
        {
            await DatabaseSetup.EnsureUserDocumentExists(UserId, documentId, InitialCategoryDocument);
        }

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            string documentId = runParams.DocumentId ?? DefaultDocumentId;

            await SetRunPrerequisites(documentId);

            string attachmentName = runParams.AttachmentName;
            bool attachmentExists;
            
            #region Demo
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1 
                attachmentExists = session.Advanced.Attachments.Exists(documentId, attachmentName);
                #endregion

                if (attachmentExists)
                {
                    #region Step_2 
                    using (var attachmentResult = session.Advanced.Attachments.Get(documentId, attachmentName))
                    #endregion
                    {
                        #region Step_3
                        var contentType = attachmentResult.Details.ContentType;
                        var hash = attachmentResult.Details.Hash;
                        var size = attachmentResult.Details.Size;
                        #endregion
                        
                        #region Step_4
                        var readBuffer = new byte[size];
                        using (var attachmentMemoryStream = new MemoryStream(readBuffer))
                        {
                            attachmentResult.Stream.CopyTo(attachmentMemoryStream);
                        }
                        #endregion
                    }
                }
            }
            #endregion

            if (attachmentExists)
            {
                return Ok($"Attachment {attachmentName} was loaded successfully");
            }
            
            return Ok($"Attachment {attachmentName} doesn't exist on document {documentId}");
        }
    }
    
    public class RunParams
    {
        public string DocumentId { get; set; }
        public string AttachmentName { get; set; }
    }
}

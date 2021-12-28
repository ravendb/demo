using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.Attachments.IndexAttachmentDetails
{
    public class IndexAttachmentDetailsController : DemoCodeController
    {
        public IndexAttachmentDetailsController(UserIdContainer userId, UserStoreCache userStoreCache,
            MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup) : base(userId, userStoreCache,
            mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class Employees_ByAttachmentDetails : AbstractIndexCreationTask<Employee, Employees_ByAttachmentDetails.Result>
        #endregion
        {
            #region Step_2
            public class Result
            {
                public string[] AttachmentNames { get; set; }
                public string[] AttachmentContentTypes { get; set; }
                public string[] AttachmentHashes { get; set; }
                public long[] AttachmentSizes { get; set; }
            }
            #endregion

            public Employees_ByAttachmentDetails()
            {
                Map = employees => from employee in employees
                    #region Step_3
                    let attachments = AttachmentsFor(employee)
                    #endregion
                    
                    #region Step_4
                    select new Result
                    {
                        AttachmentNames = attachments.Select(x => x.Name).ToArray(),
                        AttachmentContentTypes = attachments.Select(x => x.ContentType).ToArray(),
                        AttachmentHashes = attachments.Select(x => x.Hash).ToArray(),
                        AttachmentSizes = attachments.Select(x => x.Size).ToArray()
                    };
                    #endregion
            }
        }
        #endregion
        
        protected override Task SetDemoPrerequisites() => new Employees_ByAttachmentDetails().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            string attachmentContentType = runParams.AttachmentContentType ?? "image/jpeg";
            long attachmentMinSize = runParams.AttachmentMinSize ?? 18000;

            #region Demo
            List<Employee> employeesWithMatchingAttachments;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                employeesWithMatchingAttachments = session.Query<Employees_ByAttachmentDetails.Result, Employees_ByAttachmentDetails>()
                    .Where(employee => employee.AttachmentContentTypes.Contains(attachmentContentType) &&
                                       employee.AttachmentSizes.Any(x => x > attachmentMinSize))
                    .OfType<Employee>()
                    .ToList();
                #endregion
            }
            #endregion
            
            return Ok(employeesWithMatchingAttachments);
        }
    }
    
    public class RunParams
    {
        public string AttachmentContentType { get; set; }
        public long? AttachmentMinSize { get; set; }
    }
}

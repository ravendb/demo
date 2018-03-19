using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/deletingAttachments")]
        [Demo("Deleting Attachments", DemoOutputType.String, demoOrder: 216)]
        public object DeletingAttachments(string attachmentName = "the_big_cheese_logo.png")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Advanced.Attachments.Delete("companies/77-A", attachmentName);

                session.SaveChanges();
            }
            
            return $"{attachmentName} deleted successfully";
        }
    }
}

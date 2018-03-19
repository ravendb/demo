using DemoServer.Controllers;
using System.IO;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/storingAttachments")]
        [Demo("Storing Attachments", DemoOutputType.String, demoOrder: 215)]
        public object StoringAttachments(string attachmentName = "the_big_cheese_logo.png",
            string attachmentPath = "Images/big_cheese_logo.png")
        {
            using (var fileStream = new FileStream(attachmentPath, FileMode.Open))
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    session.Advanced.Attachments.Store("companies/77-A",
                        name: attachmentName, stream: fileStream, contentType: "image/png");

                    session.SaveChanges();
                }
            }

            return $"{attachmentName} attached successfully";
        }
    }
}

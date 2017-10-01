using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/store")]
        [Demo("Store", DemoOutputType.String, demoOrder: 10)]
        public object Store()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new Company
                {
                    Name = "Hibernating Rhinos",
                    ExternalId = "HR",
                    Phone = "+972 4 622 7811",
                    Fax = "+972 153 4 622 7811",
                });

                session.SaveChanges();

                return "New Company Created Successfully";
            }
        }
    }
}


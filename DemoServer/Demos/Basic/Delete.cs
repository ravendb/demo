using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/delete")]
        [Demo("Delete", DemoOutputType.String, demoOrder: 30)]
        public object Delete(string companyId = "companies/2-A")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Delete(session.Load<Company>(companyId));

                session.SaveChanges();

                return $"{companyId} Deleted Successfully";
            }
        }
    }
}


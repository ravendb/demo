using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/edit")]
        [Demo("Edit", DemoOutputType.String, demoOrder: 20)]
        public object Edit(string companyId = "companies/1-A")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var company = session.Load<Company>(companyId);

                company.Address.Line2 = "Zip 12345";

                session.SaveChanges();

                return $"{companyId} Edited Successfully";
            }
        }

    }
}


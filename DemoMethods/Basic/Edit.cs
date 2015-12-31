using System;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {

        [HttpGet]
        [Demo("Edit", DemoOutputType.String, demoOrder: 20)]
        public object Edit(int companyId = 1)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var company = session.Load<Company>(companyId);

                company.Address.Line2 = "Zip 12345";

                session.SaveChanges();

                return $"CompanyId {companyId} Edited Successfully";
            }
        }

    }
}


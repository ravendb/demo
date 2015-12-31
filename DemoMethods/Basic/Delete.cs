using System;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {
        [HttpGet]
        [Demo("Delete", DemoOutputType.String, demoOrder: 30)]
        public object Delete(int companyId = 2)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Delete(session.Load<Company>(companyId));

                session.SaveChanges();

                return $"CompanyId {companyId} Deleted Successfully";
            }
        }
    }
}


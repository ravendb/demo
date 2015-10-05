using System;
using System.Web.Http;
using DemoMethods.Entities;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
      
        [HttpGet]
        public object Edit(int c)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                // Edit :
                var company = session.Load<Company>(c);
                company.Address.Line2 = "Zip 12345";
             
                session.SaveChanges();

                return (company.Id);
            }
        }

    }
}


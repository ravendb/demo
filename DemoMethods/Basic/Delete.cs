using System;
using System.Web.Http;
using DemoMethods.Entities;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {

        [HttpGet]
        public object Delete(int c)
        {
        
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Delete(session.Load<Company>(c));
                session.SaveChanges();

                return "deleted";
            }
        }
    }
}


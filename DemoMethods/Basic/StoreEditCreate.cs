using DemoMethods.Entities;
using Raven.Client;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController 
    {
        [HttpGet]
        public object StoreEditDelete()
        {
            var contact = new Contact();
            contact.Name = "Demo";
            contact.Title = "Mr.";

            var address = new Address();
            address.Country = "IL";
            address.City = "Hadera";
            address.Line1 = "Kombe 10";

            var newCompany = new Company();
            newCompany.Name = "Hibernating Rhinos";
            newCompany.ExternalId = "HR";
            newCompany.Phone = "+972 4 622 7811";
            newCompany.Fax = "+972 153 4 622 7811";
            newCompany.Contact = contact;
            newCompany.Address = address;

            using (IDocumentSession session = Store.OpenSession())
            {
                // Store :
                session.Store(newCompany);
                session.SaveChanges();
                string newId = session.Advanced.GetDocumentId(newCompany);

                // Edit :
                var company = session.Load<Company>(newId);
                company.Address.Line2 = "Zip 12345";
                session.Store(company);

                // Delete :
                session.Delete(company);
                session.SaveChanges();

                return DemoUtilities.ObjectToJson(company);
            }
        }

    }
}

         
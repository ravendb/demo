using System.Web.Http;
using DemoMethods.Entities;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController 
    {
        [HttpGet]
        public object StoreEditDelete()
        {
            var contact = new Contact
            {
                Name = "Demo",
                Title = "Mr."
            };

            var address = new Address
            {
                Country = "IL",
                City = "Hadera",
                Line1 = "Kombe 10"
            };

            var newCompany = new Company
            {
                Name = "Hibernating Rhinos",
                ExternalId = "HR",
                Phone = "+972 4 622 7811",
                Fax = "+972 153 4 622 7811",
                Contact = contact,
                Address = address
            };

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                // Store :
                session.Store(newCompany);
                session.SaveChanges();
                var newId = session.Advanced.GetDocumentId(newCompany);

                // Edit :
                var company = session.Load<Company>(newId);
                company.Address.Line2 = "Zip 12345";
                session.Store(company);

                // Delete :
                session.Delete(company);
                session.SaveChanges();

                return (company);
            }
        }

    }
}

         
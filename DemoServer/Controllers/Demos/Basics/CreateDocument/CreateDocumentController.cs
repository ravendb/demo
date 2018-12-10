using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.CreateDocument
{
    public class CreateDocumentController : DemoCodeController
    {
        public CreateDocumentController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var companyName = runParams.CompanyName;
            var companyPhone = runParams.CompanyPhone;
            var contactName = runParams.ContactName;
            var contactTitle = runParams.ContactTitle;

            #region Demo 
            
            #region Step_1
            var newCompany = new Company
            {
                Name = companyName,
                Phone = companyPhone,
                Contact = new Contact
                {
                    Name = contactName,
                    Title = contactTitle
                }
            };
            #endregion

            #region Step_2
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            {
                #region Step_3
                session.Store(newCompany);
                #endregion
                #region Step_4
                session.SaveChanges();
                #endregion
            }
            #endregion

            return Ok("The document was created successfully");
        }

        private class Company
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public Contact Contact { get; set; }
        }

        private class Contact
        {
            public string Name { get; set; }
            public string Title { get; set; }
        }

        public class RunParams
        {
            public string CompanyName { get; set; }
            public string CompanyPhone { get; set; }
            public string ContactName { get; set; }
            public string ContactTitle { get; set; }
        }
    }
}

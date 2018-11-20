using DemoServer.Utils;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
#endregion

namespace DemoServer.Controllers.Demos.Basics.CreateDocument
{
    public class CreateDocumentController : DemoCodeController
    {
        public CreateDocumentController(HeadersAccessor headersAccessor, DatabaseAccessor databaseAccessor) : base(
            headersAccessor, databaseAccessor)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var companyName = runParams.CompanyName;
            var companyPhone = runParams.CompanyPhone;
            var contactName = runParams.ContactName;
            var contactTitle = runParams.ContactTitle;

            var serverUrl = DatabaseAccessor.GetFirstDatabaseUrl();
            var databaseName = DatabaseAccessor.GetDatabaseName(UserId);
            
            #region Demo
            
            #region Step_1
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverUrl }, 
                Database = databaseName
            };
           
            documentStore.Initialize();
            #endregion
            
            #region Step_2
            // Define the object to be stored 
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

            #region Step_3
            // Open the session for work
            using (var session = documentStore.OpenSession())
            #endregion
            {
                #region Step_4
                // Store the new company entity in the session
                session.Store(newCompany);
                #endregion
                
                #region Step_5
                // Save the entity as a document in the database
                session.SaveChanges();
                #endregion
            }
            #endregion

            return Ok("The document was created successfully");
        }

        public class Company
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public Contact Contact { get; set; }
        }

        public class Contact
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

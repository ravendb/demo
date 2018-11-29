using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
#endregion

namespace DemoServer.Controllers.Demos.Basics.EditDocument
{
    public class EditDocumentController : DemoCodeController
    {
        private const string DocumentId = "companies/1-A";

        public EditDocumentController(HeadersAccessor headersAccessor, DatabaseAccessor databaseAccessor) : base(
            headersAccessor, databaseAccessor)
        {
        }

        private Company InitialCompany => new Company
        {
            Id = DocumentId,
            Name = "Company Name",
            Phone = "(+972)52-5486969"
        };

        protected override Task SetDemoPrerequisites()
        {
            return DatabaseAccessor.SaveDocument(UserId, InitialCompany); 
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var companyName = runParams.CompanyName;

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
            // Open the session for work
            using (var session = documentStore.OpenSession())
            #endregion
            {
                #region Step_3
                // Load the document 
                var company = session.Load<Company>(DocumentId);
                #endregion
                
                #region Step_4
                // Update the data 
                company.Name = companyName;
                #endregion
                
                #region Step_5
                // Save the entity as a document in the database
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"Document {DocumentId} was edited successfully");
        }

        public class Company
        {
            public string Id { get; set; }
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
        }
    }
}

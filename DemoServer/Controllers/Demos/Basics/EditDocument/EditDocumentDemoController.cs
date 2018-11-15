using System.Threading.Tasks;
using DemoServer.Utils;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
#endregion

namespace DemoServer.Controllers.Demos.Basics.EditDocument
{
    public class EditDocumentDemoController : DemoCodeController
    {
        public EditDocumentDemoController(HeadersAccessor headersAccessor) : base(headersAccessor)
        {
        }

        public override Task SetPrerequisites()
        {
            return Task.CompletedTask;
            //TODO: Set up the database if does not exist
            //TODO: Verify the document exists in the database
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var documentID = runParams.DocumentID;
            var companyName = runParams.CompanyName;

            var serverURL = "http://localhost:8080";
            var databaseName = "DemoExample";
         
            #region Demo
            
            #region Step_1
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverURL }, 
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
                var company = session.Load<Company>(documentID);
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
            
            return Ok($"Document {documentID} was edited successfully");
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
            public string DocumentID { get; set; }
            public string CompanyName { get; set; }
        }
    }
}

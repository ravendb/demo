using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.EditDocument
{
    public class EditDocumentController : DemoCodeController
    {
        private const string DocumentId = "companies/1-A";

        public EditDocumentController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
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

            #region Demo
            
            #region Step_1
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            {
                #region Step_2
                var company = session.Load<Company>(DocumentId);
                #endregion
                
                #region Step_3
                company.Name = companyName;
                #endregion
                
                #region Step_4
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"Document {DocumentId} was edited successfully");
        }

        private class Company
        {
            public string Id { get; set; }
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
        }
    }
}

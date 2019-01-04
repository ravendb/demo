using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.CreateDocument
{
    public class CreateDocumentController : DemoCodeController
    {
        public CreateDocumentController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var companyName = runParams.CompanyName;
            var companyPhone = runParams.CompanyPhone;
            var contactName = runParams.ContactName;
            var contactTitle = runParams.ContactTitle;

            string theNewDocumentId;

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

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_2
                session.Store(newCompany);
                #endregion
                
                #region Step_3
                theNewDocumentId = session.Advanced.GetDocumentId(newCompany);
                #endregion
                
                #region Step_4
                session.SaveChanges();
                #endregion
            }
            #endregion

            return Ok($"Document {theNewDocumentId} was created successfully");
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

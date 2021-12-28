using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Basics.CreateDocument
{
    public class CreateDocumentController : DemoCodeController
    {
        public CreateDocumentController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string companyName = runParams.CompanyName?? "Hibernating Rhinos";
            string companyPhone = runParams.CompanyPhone?? "(+972)52-5486969";
            string contactName = runParams.ContactName?? "New Contact Name";
            string contactTitle = runParams.ContactTitle?? "New Contact Title";

            string theNewDocumentId;

            #region Demo 
            #region Step_1
            Company newCompany = new Company
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

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_2
                session.Store(newCompany);
                #endregion
                
                #region Step_3
                theNewDocumentId = newCompany.Id;
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

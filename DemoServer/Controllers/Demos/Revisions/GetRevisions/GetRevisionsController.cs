using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Operations.Revisions;
#endregion

namespace DemoServer.Controllers.Demos.Revisions.GetRevisions
{
    public class GetRevisionsController : DemoCodeController
    {
        public GetRevisionsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
       
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            #region Step_1
            RevisionsConfiguration myRevisionsConfiguration = new RevisionsConfiguration
            {
               Default = new RevisionsCollectionConfiguration
               {
                   Disabled = false
               }
            };
          
            ConfigureRevisionsOperation revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
            DocumentStoreHolder.Store.Maintenance.Send(revisionsConfigurationOperation);
            #endregion

            List<Company> revisions;
                
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_2
                Company company = session.Load<Company>("companies/7-A");

                company.Name = "Name 1";
                session.CountersFor("companies/7-A").Increment("MyCounter", 100);
                session.SaveChanges();
                
                company.Name = "Name 2";
                company.Phone = "052-1234-567";
                session.SaveChanges();
                #endregion
                
                #region Step_3
                revisions = session
                    .Advanced
                    .Revisions
                    .GetFor<Company>("companies/7-A", start: 0, pageSize: 25);
                #endregion
            }
            #endregion
            
            return Ok(revisions);
        }
    }
}

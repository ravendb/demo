using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
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
        public GetRevisionsController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
       
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            RevisionsConfiguration myRevisionsConfiguration = new RevisionsConfiguration
            {
               Default = new RevisionsCollectionConfiguration
               {
                   Disabled = false
               }
            };
          
            ConfigureRevisionsOperation revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
            DocumentStoreHolder.Store.Maintenance.Send(revisionsConfigurationOperation);

            List<Company> revisions;
                
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                
                Company company = session.Load<Company>("companies/7-A");

                company.Name = "Name 1";
                session.SaveChanges();
                
                company.Name = "Name 2";
                company.Phone = "052-1234-567";
                session.SaveChanges();

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

using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.ProjectingIndividualFields
{
    public class ProjectingIndividualFieldsController : DemoCodeController
    {
        public ProjectingIndividualFieldsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        private class CompanyDetails
        {
            public string CompanyName { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
        
        [HttpPost]
        public IActionResult Run()
        {
            object projectedResults;

            #region Demo
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<CompanyDetails> projectedQuery = session.Query<Company>()
                #endregion 
                #region Step_2
                    .Select(x => new CompanyDetails
                    {
                          CompanyName = x.Name,
                          City = x.Address.City,
                          Country = x.Address.Country,
                    });
                #endregion
                
                #region Step_3
                projectedResults = projectedQuery.ToList();
                #endregion
            }
            #endregion 
            
            return Ok(projectedResults); 
        }
    }
}

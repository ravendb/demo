using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.QueryOverview
{
    public class QueryOverviewController : DemoCodeController
    {
        public QueryOverviewController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public void Run()
        {
            #region Demo
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<Employee> queryDefinition = session.Query<Employee>();
                #endregion

                #region Step_2
                // Define actions such as:
                    
                // Filter documents by documents fields
                // Filter documents by text criteria 
                // Include related documents
                // Get the query stats
                // Sort results 
                // Customise the returned entity fields (Projections)
                // Control results paging  
                #endregion
                
                #region Step_3
                List<Employee> queryResults = queryDefinition.ToList();
                #endregion
            }
            
            #endregion
        }
    }
}

using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.QueryOverview
{
    public class QueryOverviewController : DemoCodeController
    {
        public QueryOverviewController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public void Run()
        {
            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var queryDefinition = session.Query<Employee>();
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
                var queryResults = queryDefinition.ToList();
                #endregion
            }
            
            #endregion
        }
    }
}

using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
#endregion

namespace DemoServer.Controllers.Demos.Queries.FullCollectionQuery
{
    public class FullCollectionQueryController : DemoCodeController
    {
        public FullCollectionQueryController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        private async Task SetRunPrerequisites()
        {
            var documentsToStore = new List<Company>
            {
                new Company {Id = "companies/1", Name = "Name1", Phone = "Phone1"},
                new Company {Id = "companies/2", Name = "Name2", Phone = "Phone2"},
                new Company {Id = "companies/3", Name = "Name3", Phone = "Phone3"},
                new Company {Id = "companies/4", Name = "Name4", Phone = "Phone4"},
                new Company {Id = "companies/5", Name = "Name5", Phone = "Phone5"}
            };

            await DatabaseSetup.EnsureUserCollectionExists(UserId, documentsToStore);
        }
        
        [HttpPost]
        public async Task<IActionResult> Run()
        {
            await SetRunPrerequisites();
            
            #region Demo
            List<Company> collectionResults;

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var fullCollectionQuery = session.Query<Company>();
                #endregion
                
                #region Step_2
                collectionResults = fullCollectionQuery.ToList();
                #endregion
            }
            
            #endregion 
            
            return Ok(collectionResults);
        }
    }
}

using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

#region Usings
using System.Collections.Generic;
using System.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.SimpleCollectionQuery
{
    public class SimpleCollectionQueryController : DemoCodeController
    {
        public SimpleCollectionQueryController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
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

            await DatabaseSetup.EnsureCollectionExists(UserId, documentsToStore);
        }
        
        [HttpPost]
        public async Task<IActionResult> Run()
        {
            await SetRunPrerequisites();
            
            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IList<Company> results = session.Query<Company>()
                    .ToList();
                #endregion
            }
            
            #endregion 
            
            //TODO: How to show results ?
            return Ok("Employee collection query results are: ...  TODO: Show Query Results ..."); 
        }
    }
}

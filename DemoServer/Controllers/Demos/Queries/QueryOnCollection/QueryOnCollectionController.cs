using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Collections.Generic;
using System.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.QueryOnCollection
{
    public class QueryOnCollectionController : DemoCodeController
    {
        public QueryOnCollectionController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }

        private async Task SetRunPrerequisites()
        {
            IList<Company> results = DocumentStoreHolder.Store.OpenSession().Query<Company>()
                .ToList();
        
            // Create some documents if collection is empty
            if (results.Count == 0)
            {
                IList<Company> documentsToStore = new List<Company>()
                {
                    new Company() {Id = "Companies/1", Name = "Name1", Phone = "Phone1"},
                    new Company() {Id = "Companies/2", Name = "Name2", Phone = "Phone2"},
                    new Company() {Id = "Companies/3", Name = "Name3", Phone = "Phone3"},
                    new Company() {Id = "Companies/4", Name = "Name4", Phone = "Phone4"},
                    new Company() {Id = "Companies/5", Name = "Name5", Phone = "Phone5"}
                }; 
                
                await DatabaseAccessor.BulkInsertDocuments<Company>(UserId, documentsToStore); 
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Run()
        {
            await SetRunPrerequisites();
            
            #region Demo
            
            #region Step_1
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            {
                #region Step_2
                IList<Company> results = session.Query<Company>()
                    .ToList();
                #endregion
            }
            
            #endregion 
            
            //TODO: How to show results ?
            return Ok($"Employee collection query results are: ...  TODO: Show Query Results ..."); 
        }
        
        private class Company
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
        }
    }
}

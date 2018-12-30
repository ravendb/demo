using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.FilteringQueryResults
{
    public class FilteringQueryResultsController : DemoCodeController
    {
        public FilteringQueryResultsController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var country = runParams.Country;
            
            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var filteredQuery = session.Query<Employee>()
                #endregion
                #region Step_2
                      .Where( x =>
                                x.FirstName.In("Anne", "John")  ||
                                
                               (x.Address.Country == country    &&
                                x.Territories.Count > 2         &&
                                x.Title.StartsWith("Sales")));
                #endregion
                
                #region Step_3
                var filteredEmployees = filteredQuery.ToList();
                #endregion
            }
            
            #endregion 
            
            //TODO: How to show results ?
            return Ok($"Query results are: ... TODO: Show Query Results ..."); 
        }
        
        public class RunParams
        {
            public string Country { get; set; }
        }
    }
}

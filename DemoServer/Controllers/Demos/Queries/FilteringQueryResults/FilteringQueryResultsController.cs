using System.Linq;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
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
                var query = session.Query<Employee>().Where( x =>
                                x.FirstName.In("Anne", "John")  ||
                               (x.Address.Country == country    &&
                                x.Territories.Count > 2         &&
                                x.Title.StartsWith("Sales")));
                
                var employee = query.ToList();
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

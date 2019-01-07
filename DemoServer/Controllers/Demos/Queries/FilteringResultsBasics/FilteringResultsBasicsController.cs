using System.Collections.Generic;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.FilteringResultsBasics
{
    public class FilteringResultsBasicsController : DemoCodeController
    {
        public FilteringResultsBasicsController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var country = runParams.Country;
            
            #region Demo
            List<Employee> filteredEmployees;

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<Employee> filteredQuery = session.Query<Employee>()
                    #endregion
                    #region Step_2
                    .Where(x => x.FirstName == "Anne");
                #endregion
                
                #region Step_3
                filteredEmployees = filteredQuery.ToList();
                #endregion
            }
            #endregion 
            
            return Ok(filteredEmployees);
        }
        
        public class RunParams
        {
            public string Country { get; set; }
        }
    }
}

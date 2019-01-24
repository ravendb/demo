using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.FilteringResultsMultipleConditions
{
    public class FilteringResultsMultipleConditions : DemoCodeController
    {
        public FilteringResultsMultipleConditions(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string country = runParams.Country;
            
            #region Demo
            List<Employee> filteredEmployees;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<Employee> filteredQuery = session.Query<Employee>()
                    #endregion
                    #region Step_2
                    .Where( x =>
                        x.FirstName.In("Anne", "John")  ||
                                
                        (x.Address.Country == country    &&
                         x.Territories.Count > 2         &&
                         x.Title.StartsWith("Sales")));
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

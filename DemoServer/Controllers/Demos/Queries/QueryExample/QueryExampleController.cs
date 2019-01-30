using System;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Queries;
#endregion

namespace DemoServer.Controllers.Demos.Queries.QueryExample
{
    public class QueryExampleController : DemoCodeController
    {
        public QueryExampleController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        public class EmployeeDetails
        {
            public string EmployeeName { get; set; }
            public string Title { get; set; }
            public DateTime HiredAt { get; set; }
            public string ManagerName { get; set; }
        }
        
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            List<EmployeeDetails> queryResults;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                QueryStatistics statistics;
                
                #region Step_1
                IQueryable<EmployeeDetails> query = session.Query<Employee>()
                    #endregion
                    #region Step_2
                    .Where(x => x.FirstName == "Steven" || 
                                x.Title == "Sales Representative")
                    #endregion
                    #region Step_3
                    .Include(x => x.ReportsTo)
                    #endregion
                    #region Step_4
                    .Statistics(out statistics)
                    #endregion
                    #region Step_5
                    .OrderByDescending(x => x.HiredAt)
                    #endregion
                    #region Step_6
                    .Select(x => new EmployeeDetails
                    {
                        EmployeeName = $"{x.FirstName} {x.LastName}",
                        Title = x.Title,
                        HiredAt = x.HiredAt,
                        ManagerName = RavenQuery.Load<Employee>(x.ReportsTo).FirstName + " " +
                                      RavenQuery.Load<Employee>(x.ReportsTo).LastName,
                    })
                    #endregion
                    #region Step_7
                    .Take(5);
                    #endregion
                
                #region Step_8
                queryResults = query.ToList();
                #endregion
            }
            #endregion

            return Ok(queryResults);
        }
    }
}

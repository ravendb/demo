using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.QueryOverview
{
    public class QueryOverviewController : DemoCodeController
    {
        public QueryOverviewController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        public class EmployeeName
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
        
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            List<EmployeeName> queryResults;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                QueryStatistics statistics;

                IQueryable<EmployeeName> query = session.Query<Employee>()
                #endregion
                #region Step_2
                    .Where(x => x.FirstName == "Robert" || x.Title == "Sales Representative")
                    .Include(x => x.ReportsTo)
                    .Statistics(out statistics)
                    .OrderByDescending(x => x.HiredAt)
                    .Select(x => new EmployeeName
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    })
                    .Take(10);
                 #endregion
                
                #region Step_3
                queryResults = query.ToList();
                #endregion
            }
            #endregion

            return Ok(queryResults);
        }
    }
}

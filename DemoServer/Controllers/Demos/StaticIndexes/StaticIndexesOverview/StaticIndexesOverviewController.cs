using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.StaticIndexes.StaticIndexesOverview
{
    public class StaticIndexesOverviewController : DemoCodeController
    {
        public StaticIndexesOverviewController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Employees_ByLastName : AbstractIndexCreationTask<Employee, Employees_ByLastName.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string LastName { get; set; }
            }
            #endregion
            
            #region Step_3
            public Employees_ByLastName()
            {
                // Define:
                //    Map(s) functions
                //    Reduce function
                //    Additional indexing options per field
            }
            #endregion
        }
        #endregion

        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            List<Employee> queryResults;
            
            #region Step_4
            new Employees_ByLastName().Execute(DocumentStoreHolder.Store);
            #endregion
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                IQueryable<Employee> queryOnIndex = session.Query<Employees_ByLastName.IndexEntry, Employees_ByLastName>()
                      .Where(employee => employee.LastName == "SomeName")
                      .OfType<Employee>();

                queryResults = queryOnIndex.ToList();
                #endregion
            }
            #endregion

            return Ok(queryResults);
        }
    }
}

using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.StaticIndexes.StaticIndexesOverview
{
    public class StaticIndexesOverviewController : DemoCodeController
    {
        public StaticIndexesOverviewController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        
        #region Step_1
        public class Employees_ByLastName : AbstractIndexCreationTask<Employee, Employees_ByLastName.Result>
        #endregion
        {
            #region Step_2
            public class Result
            {
                public string lastName { get; set; }
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
        
        [HttpPost]
        public void Run()
        {
            #region Step_4
            new Employees_ByLastName().Execute(DocumentStoreHolder.Store);
            #endregion
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                var queryOnIndex = session.Query<Employees_ByLastName.Result, Employees_ByLastName>()
                      .Where(x => x.lastName == "SomeName")
                      .ToList();
                #endregion
            }
        }
        #endregion
    }
}

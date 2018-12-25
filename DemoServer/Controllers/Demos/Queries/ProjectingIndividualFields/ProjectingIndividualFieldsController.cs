using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
#endregion

namespace DemoServer.Controllers.Demos.Queries.ProjectingIndividualFields
{
    public class ProjectingIndividualFieldsController : DemoCodeController
    {
        public ProjectingIndividualFieldsController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            #region Demo
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var projectedQuery = session.Query<Company>()
                #endregion 
                #region Step_2
                    .Select(x => new {
                          CompanyName = x.Name,
                          City = x.Address.City,
                          Country = x.Address.Country,
                    });
                #endregion
                
                #region Step_3
                var projectedResults = projectedQuery.ToList();
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

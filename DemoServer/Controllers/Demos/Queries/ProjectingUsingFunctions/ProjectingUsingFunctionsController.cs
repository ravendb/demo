using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System;
using System.Linq;
#endregion


namespace DemoServer.Controllers.Demos.Queries.ProjectingUsingFunctions
{
    public class ProjectingUsingFunctionsController : DemoCodeController
    {
        public ProjectingUsingFunctionsController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var projectedQueryWithFunctions = (from employee in session.Query<Employee>()
                #endregion
                
                #region Step_2
                    let formatTitle = (Func<Employee, string>)(e => "Title: " + e.Title)
                    let formatName = (Func<Employee, string>)(e => "Name: " + e.FirstName + " " + e.LastName)
                #endregion
                
                #region Step_3
                    select new
                    {
                       Title = formatTitle(employee),
                       Name = formatName(employee)
                    });
                #endregion

                #region Step_4
                var projectedResults = projectedQueryWithFunctions.ToList();
                #endregion
            }
            #endregion 
            
            //TODO: How to show results ?
            return Ok($"Query results are: ... TODO: Show Query Results ..."); 
        }
    }
}

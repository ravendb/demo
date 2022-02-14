using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System;
using System.Linq;
using Raven.Client.Documents.Session;
#endregion


namespace DemoServer.Controllers.Demos.Queries.ProjectingUsingFunctions
{
    public class ProjectingUsingFunctionsController : DemoCodeController
    {
        public ProjectingUsingFunctionsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        private class EmployeeDetails
        {
            public string Title { get; set; }
            public string FullName { get; set; }
        }
        
        [HttpPost]
        public IActionResult Run()
        {
            object projectedResults;

            #region Demo
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                IQueryable<EmployeeDetails> projectedQueryWithFunctions = from employee in session.Query<Employee>()
                #endregion
                
                #region Step_2
                    let formatTitle = (Func<Employee, string>)(e => "Title: " + e.Title)
                    let formatName = (Func<Employee, string>)(e => "FullName: " + e.FirstName + " " + e.LastName)
                #endregion
                
                #region Step_3
                    select new EmployeeDetails
                    {
                       Title = formatTitle(employee),
                       FullName = formatName(employee)
                    };
                #endregion

                #region Step_4
                projectedResults = projectedQueryWithFunctions.ToList();
                #endregion
            }
            #endregion 
            
            return Ok(projectedResults);
        }
    }
}

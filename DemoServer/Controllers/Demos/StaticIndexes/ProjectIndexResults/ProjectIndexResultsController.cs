using System.Threading.Tasks;
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

namespace DemoServer.Controllers.Demos.StaticIndexes.ProjectIndexResults
{
    public class ProjectIndexResultsController : DemoCodeController
    {
        public ProjectIndexResultsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Employees_ByWorkPeriod : AbstractIndexCreationTask<Employee, Employees_ByWorkPeriod.IndexEntry>
            #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public int WorkingInCompanySince { get; set; }
            }
            #endregion
            
            #region Step_3
            public class EmployeeProjectedDetails
            {
                public string FirstName { get; set; }
                public string Phone { get; set; }
                public string Location { get; set; }
            }
            #endregion
            
            
            #region Step_4
            public Employees_ByWorkPeriod()
            {
                Map = employees => from employee in employees
                    select new IndexEntry
                    {
                        WorkingInCompanySince =  employee.HiredAt.Year
                    };
            }
            #endregion
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Employees_ByWorkPeriod().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int startYear = runParams.StartYear?? 1993;

            #region Demo
            List<Employees_ByWorkPeriod.EmployeeProjectedDetails> employeesSinceYear;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                var employeesQuery = session

                    #region Step_5
                    .Query<Employees_ByWorkPeriod.IndexEntry, Employees_ByWorkPeriod>()
                    .Where(employee => employee.WorkingInCompanySince > startYear)
                    #endregion
                    #region Step_6
                    .OfType<Employee>()
                    .Select(employee => new Employees_ByWorkPeriod.EmployeeProjectedDetails
                    {
                        FirstName = employee.FirstName,
                        Phone = employee.HomePhone,
                        Location = employee.Address.City + ' ' + employee.Address.Country
                    });
                    #endregion

                #region Step_7
                employeesSinceYear = employeesQuery.ToList();
                #endregion
            }
            #endregion

            return Ok(employeesSinceYear);
        }
        
        public class RunParams
        {
            public int? StartYear { get; set; }
        }
    }
}

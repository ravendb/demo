using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.StaticIndexes.MapIndex
{
    public class MapIndexController : DemoCodeController
    {
        public MapIndexController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Employees_ImportantDetails : AbstractIndexCreationTask<Employee, Employees_ImportantDetails.Result>
            #endregion
        {
            #region Step_2
            public class Result
            {
                public string FullName { get; set; }
                public string Country { get; set; }
                public int    WorkingInCompanySince { get; set; }
                public int    NumberOfTerritories { get; set; }
            }
            #endregion
            
            #region Step_3
            public Employees_ImportantDetails()
            {
                Map = employees => from employee in employees
                    select new Result
                    {
                       FullName = employee.FirstName + " " + employee.LastName,
                       Country = employee.Address.Country,
                       WorkingInCompanySince =  employee.HiredAt.Year,
                       NumberOfTerritories = employee.Territories.Count,
                    };
            }
            #endregion
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Employees_ImportantDetails().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int startYear = runParams.StartYear;

            #region Demo
            List<Employee> employeesFromUSA;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_4
                employeesFromUSA = session.Query<Employees_ImportantDetails.Result, Employees_ImportantDetails>()
                       .Where(employee => employee.Country == "USA" &&
                                          employee.WorkingInCompanySince > startYear)
                       .OfType<Employee>()
                       .ToList();
                #endregion
            }
            #endregion

            return Ok(employeesFromUSA);
        }
        
        public class RunParams
        {
            public int StartYear { get; set; }
        }
    }
}

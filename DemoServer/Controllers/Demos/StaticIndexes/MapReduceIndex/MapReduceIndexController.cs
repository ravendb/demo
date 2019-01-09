using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.StaticIndexes.MapReduceIndex
{
    public class MapReduceIndexController : DemoCodeController
    {
        public MapReduceIndexController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Employees_ByCountry : AbstractIndexCreationTask<Employee, Employees_ByCountry.Result>
        #endregion
        {
            #region Step_2
            public class Result
            {
                public string Country { get; set; }
                public int CountryCount { get; set; }
            }
            #endregion
            
            public Employees_ByCountry()
            {
                #region Step_3
                Map = employees => from employee in employees
                    select new Result
                    {
                       Country = employee.Address.Country,
                       CountryCount = 1
                    };
                #endregion
                
                #region Step_4
                Reduce = results => from result in results
                    group result by result.Country into g
                    select new Result
                    {
                        Country = g.Key,
                        CountryCount = g.Sum(x => x.CountryCount)
                    };
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Employees_ByCountry().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string country = runParams.Country;
            int numberOfEmployeesFromCountry;

            #region Demo
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                Employees_ByCountry.Result queryResult = session.Query<Employees_ByCountry.Result, Employees_ByCountry>()
                      .FirstOrDefault(x => x.Country == country);
                    
                numberOfEmployeesFromCountry = queryResult?.CountryCount ?? 0;
                #endregion
            }
            #endregion

            return Ok($"Number of employees from : {country.ToUpper()} is: {numberOfEmployeesFromCountry}"); 
        }
        
        public class RunParams
        {
            public string Country { get; set; }
        }
    }
}

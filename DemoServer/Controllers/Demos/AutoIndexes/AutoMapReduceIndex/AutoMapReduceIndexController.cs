using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.AutoIndexes.AutoMapReduceIndex
{
    public class AutoMapReduceIndexController : DemoCodeController
    {
        public AutoMapReduceIndexController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache, 
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        public class CountryDetails
        {
            public string Country { get; set; }
            public int NumberOfEmployees { get; set; }
        }
        #endregion
        
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            List<CountryDetails> numberOfEmployeesPerCountry;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                numberOfEmployeesPerCountry = session.Query<Employee>()
                #endregion
                     #region Step_2
                    .GroupBy(x => x.Address.Country)
                     #endregion
                     #region Step_3
                    .Select(g => new CountryDetails
                    {
                        Country = g.Key,
                        NumberOfEmployees = g.Count() 
                    })
                    #endregion 
                    #region Step_4 
                    .OrderByDescending(x => x.NumberOfEmployees)
                    #endregion 
                    #region Step_5
                    .ToList();
                    #endregion
            }
            #endregion
            
            return Ok(numberOfEmployeesPerCountry);
        }
    }
}

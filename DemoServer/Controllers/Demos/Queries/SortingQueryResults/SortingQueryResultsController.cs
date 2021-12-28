using System.Collections.Generic;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Queries.SortingQueryResults
{
    public class SortingQueryResultsController : DemoCodeController
    {
        public SortingQueryResultsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            decimal numberOfUnits = runParams.NumberOfUnits?? 20;

            #region Demo
            List<Product> sortedProducts;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                sortedProducts = session.Query<Product>()
                #endregion
                     #region Step_2
                     .Where(x => x.UnitsInStock > numberOfUnits)
                     #endregion
                     #region Step_3
                     .OrderByDescending(x => x.UnitsInStock)
                     #endregion
                     #region Step_4
                     .ThenBy(x => x.Name, OrderingType.AlphaNumeric)
                     #endregion
                     #region Step_5
                     .ToList();
                     #endregion
            }
            #endregion 
            
            return Ok(sortedProducts);
        }
        
        public class RunParams
        {
            public decimal? NumberOfUnits { get; set; }
        }
    }
}

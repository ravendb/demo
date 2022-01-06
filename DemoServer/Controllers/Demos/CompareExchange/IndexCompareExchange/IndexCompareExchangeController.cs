using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Operations.CompareExchange;

#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.CompareExchange.IndexCompareExchange
{
    public class IndexCompareExchangeController : DemoCodeController
    {
        public IndexCompareExchangeController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class Products_ByUnitsInStock : AbstractIndexCreationTask<Product, Products_ByUnitsInStock.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public int UnitsInStock { get; set; }
            }
            #endregion
            
            #region Step_3
            public Products_ByUnitsInStock()
            {
                Map = products => from product in products
                    select new IndexEntry
                    {
                        UnitsInStock = LoadCompareExchangeValue<int>(product.Id)
                    };
            }
            #endregion
        }
        #endregion
        
        protected override Task SetDemoPrerequisites() => new Products_ByUnitsInStock().ExecuteAsync(DocumentStoreHolder.Store);
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int minValue = runParams.MinValue?? 25;
            
            #region Demo
            List<Product> products;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_4
                products = session.Query<Products_ByUnitsInStock.IndexEntry, Products_ByUnitsInStock>()
                    .Where(indexEntry => indexEntry.UnitsInStock > minValue)
                    .OfType<Product>()
                    .ToList();
                #endregion
            }
            #endregion

            return Ok(products);
        }
        
        public class RunParams
        {
            public int? MinValue { get; set; }
        }
    }
}

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
using Raven.Client.Documents.Linq;
#endregion

namespace DemoServer.Controllers.Demos.StaticIndexes.FanoutIndex
{
    public class FanoutIndexController : DemoCodeController
    {
        public FanoutIndexController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class Orders_ByProductDetails : AbstractIndexCreationTask<Order, Orders_ByProductDetails.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string ProductId { get; set; }
                public string ProductName { get; set; }
            }
            #endregion
            
            public Orders_ByProductDetails()
            {
                #region Step_3
                Map = orders => from order in orders
                    from orderLine in order.Lines
                    select new IndexEntry
                    {
                        ProductId = orderLine.Product,
                        ProductName = orderLine.ProductName
                    };
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new Orders_ByProductDetails().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string namePrefix = runParams.NamePrefix?? "Chocolade";

            #region Demo
            List<Order> orders;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_4
                orders = session
                    .Query<Orders_ByProductDetails.IndexEntry, Orders_ByProductDetails>()
                    .Where(order => order.ProductName.StartsWith(namePrefix))
                    .OfType<Order>()
                    .ToList();
                #endregion
            }
            #endregion

            return Ok(orders);
        }
        
        public class RunParams
        {
            public string NamePrefix { get; set; }
        }
    }
}

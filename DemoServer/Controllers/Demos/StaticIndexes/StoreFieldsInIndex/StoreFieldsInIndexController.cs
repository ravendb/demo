using System;
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
using Raven.Client.Documents;
#endregion

namespace DemoServer.Controllers.Demos.StaticIndexes.StoreFieldsInIndex
{
    public class StoreFieldsInIndexController : DemoCodeController
    {
        public StoreFieldsInIndexController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class OrdersQuantity_ByCompany : AbstractIndexCreationTask<Order, OrdersQuantity_ByCompany.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string Company { get; set; }
                public int TotalItemsOrdered { get; set; }
            }
            #endregion
            
            #region Step_3
            public class OrderProjectedDetails
            {
                public DateTime OrderedAt { get; set; }
                public int TotalItemsOrdered { get; set; }
            }
            #endregion
            
            public OrdersQuantity_ByCompany()
            {
                #region Step_4
                Map = orders => from order in orders
                    select new IndexEntry
                    {
                        Company = order.Company,
                        TotalItemsOrdered = order.Lines.Sum(orderLine => orderLine.Quantity)
                    };
                #endregion
                
                #region Step_5
                Stores.Add(x => x.TotalItemsOrdered, FieldStorage.Yes);
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new OrdersQuantity_ByCompany().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string companyId = runParams.companyId?? "companies/1-A";

            #region Demo
            List<OrdersQuantity_ByCompany.OrderProjectedDetails> ordersDetails;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                var ordersQuery = session
                    #region Step_6
                    .Query<OrdersQuantity_ByCompany.IndexEntry, OrdersQuantity_ByCompany>()
                    .Where(order => order.Company == companyId)
                    #endregion
                    #region Step_7
                    .ProjectInto<OrdersQuantity_ByCompany.OrderProjectedDetails>();
                    #endregion

                #region Step_8
                ordersDetails = ordersQuery.ToList();
                #endregion
            }
            #endregion

            return Ok(ordersDetails);
        }
        
        public class RunParams
        {
            public string companyId { get; set; }
        }
    }
}

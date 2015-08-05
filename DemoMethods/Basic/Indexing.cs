using DemoMethods.Entities;
using Raven.Client;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        public class Index_DelayedOrder : AbstractIndexCreationTask<Order>
        {
            public class Result
            {
                public TimeSpan Delay;
            }

            public Index_DelayedOrder()
            {

                Map = orders => from order in orders
                                select new
                                {
                                    Delay = order.ShippedAt - order.OrderedAt
                                };
            }
        }


        public class Index_CostlyOrders : AbstractIndexCreationTask<Order>
        {
            public class Result
            {
                public TimeSpan Delay;
                public int Price;
            }

            public Index_CostlyOrders()
            {
                Map = orders => from order in orders
                                select new
                                {
                                    Delay = order.ShippedAt - order.OrderedAt,
                                    Price = order.Lines.Sum(x => (x.Quantity * x.PricePerUnit) * (1 - x.Discount))
                                };
            }
        }

        private const int HIGH_PRICE = 150;
        private const int DELAY_DAYS = 15;

        private static IDocumentStore store = new DocumentStoreHolder().Store;
        private static IDocumentSession Session = store.OpenSession();

        [HttpGet]
        public object Indexing()
        {
            //GetImportantOrdersWithIssues

            // High Price Orders
            List<Order> highPriceOrders = Session
                .Query<Order>()
                .Where(x => x.Lines.Any(l => l.PricePerUnit > HIGH_PRICE))
                .ToList();


            new Index_DelayedOrder().Execute(store.DatabaseCommands.ForDatabase("Northwind"), store.Conventions);
            new Index_CostlyOrders().Execute(store.DatabaseCommands.ForDatabase("Northwind"), store.Conventions); 
  
            List<Order> delayingOrders = Session
                .Query<Index_CostlyOrders.Result, Index_CostlyOrders>()
                .Where(x => x.Delay > TimeSpan.FromDays(DELAY_DAYS) && x.Price > 10)
                .OfType<Order>()
                .ToList();

            return DemoUtilities.ObjectToJson(delayingOrders);
        }
    }
}

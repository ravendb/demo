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
        public class Index_CostlyOrders : AbstractIndexCreationTask<Order>
        {
            public class Result
            {
                public string OrderId;
                public TimeSpan Delay;
                public int Price;
            }

            public Index_CostlyOrders()
            {
                Map = orders => from order in orders
                                select new
                                {
                                    OrderId = order.Id,
                                    Delay = order.ShippedAt - order.OrderedAt,
                                    Price = order.Lines.Sum(x => (x.Quantity * x.PricePerUnit) * (1 - x.Discount))
                                };
            }
        }

        private const int HIGH_PRICE = 1;
        private const int DELAY_DAYS = 1;

        [HttpGet]
        public object Indexing()
        {
            new Index_CostlyOrders().Execute(Store);

            //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
            using (var session = Store.OpenSession())
            {
                var problemtaticOrders = session
                    .Query<Index_CostlyOrders.Result, Index_CostlyOrders>()                    
                    .Where(x => x.Delay > TimeSpan.FromDays(DELAY_DAYS))// && x.Price > HIGH_PRICE)
                    //.OfType<Order>()
                    .ToList();

                return DemoUtilities.Instance.ObjectToJson(problemtaticOrders);
            }
        }
    }
}

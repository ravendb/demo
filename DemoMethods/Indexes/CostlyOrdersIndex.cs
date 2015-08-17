using System;
using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class Index_CostlyOrders : AbstractIndexCreationTask<Order>
    {
        public class Result
        {
            public string OrderId;
            public TimeSpan Delay;
            public decimal Price;
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
}

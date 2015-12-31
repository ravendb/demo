using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {
        /*
        
         from order in orders
         select new
         {
             OrderId = order.Id,
             Delay = order.ShippedAt - order.OrderedAt,
             Price = order.Lines.Sum(x => (x.Quantity * x.PricePerUnit) * (1 - x.Discount))
         }; 
          
         
         */

        [HttpGet]
        public object StaticQuery(string highPrice = "500", string delayDays = "35")
        {
            var HighPrice = int.Parse(highPrice);
            var DelayDays = int.Parse(delayDays);

            //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var problemtaticOrders = session
                    .Query<CostlyOrders.Result, CostlyOrders>()
                    .Where(x => x.Price > HighPrice && x.Delay > TimeSpan.FromDays(DelayDays))
                    .OfType<Order>()
                    .ToList();

                return (problemtaticOrders);
            }
        }
    }
}

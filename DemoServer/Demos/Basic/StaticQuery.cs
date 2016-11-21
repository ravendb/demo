using System;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/staticQuery")]
        [Demo("Static Query", DemoOutputType.Flatten, demoOrder: 70)]
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

                return problemtaticOrders;
            }
        }
    }
}

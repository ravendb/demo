using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        private const int HIGH_PRICE = 500;
        private const int DELAY_DAYS = 35;

        [HttpGet]
        public object Indexing()
        {
            new Index_CostlyOrders().Execute(Store);

            //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
            using (var session = Store.OpenSession())
            {
                var problemtaticOrders = session
                    .Query<Index_CostlyOrders.Result, Index_CostlyOrders>()                    
                    .Where(x =>  x.Price > HIGH_PRICE && x.Delay > TimeSpan.FromDays(DELAY_DAYS))
                    .OfType<Order>()
                    .ToList();

                return DemoUtilities.Instance.ObjectToJson(problemtaticOrders);
            }
        }
    }
}

using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        private const int HighPrice = 500;
        private const int DelayDays = 35;

        [HttpGet]
        public object Indexing()
        {
            new IndexCostlyOrders().Execute(DocumentStoreHolder.Store);

            //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var problemtaticOrders = session
                    .Query<IndexCostlyOrders.Result, IndexCostlyOrders>()                    
                    .Where(x =>  x.Price > HighPrice && x.Delay > TimeSpan.FromDays(DelayDays))
                    .OfType<Order>()
                    .ToList();

                return DemoUtilities.Instance.ObjectToJson(problemtaticOrders);
            }
        }
    }
}

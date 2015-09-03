using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object StaticQuery()
        {
            var userParams = new NameValueCollection
            {
                {"HighPrice", "500"},
                {"DelayDays", "35"}
            };
            DemoUtilities.GetUserParameters(Request.RequestUri.Query, userParams);

            var HighPrice = int.Parse(userParams["HighPrice"]);
            var DelayDays = int.Parse(userParams["DelayDays"]);

            //GetImportantOrdersWithIssues - High Price Orders and Delayed Orders
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var problemtaticOrders = session
                    .Query<IndexCostlyOrders.Result, IndexCostlyOrders>()                    
                    .Where(x =>  x.Price > HighPrice && x.Delay > TimeSpan.FromDays(DelayDays))
                    .OfType<Order>()
                    .ToList();

                return (problemtaticOrders);
            }
        }
    }
}

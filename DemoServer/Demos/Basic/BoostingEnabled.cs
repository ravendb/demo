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
        [Route("/basic/boostingEnabled")]
        [Demo("Boosting Enabled", DemoOutputType.Flatten, demoOrder: 140)]
        public object BoostingEnabled(string city = "London", string country = "Denmark")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var orders = session.Query<Order, OrderByCompanyAndCountryWithBoost>()
                    .Where(x => x.ShipTo.City == city || x.ShipTo.Country == country)
                    .ToList();

                return orders;
            }
        }
    }
}

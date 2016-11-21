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
        [Route("/basic/boostingDisabled")]
        [Demo("Boosting Disabled", DemoOutputType.Flatten, demoOrder: 150)]
        public object BoostingDisabled(string city = "London", string country = "Denmark")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var orders = session.Query<Order, OrderByCompanyAndCountry>()
                    .Where(x => x.ShipTo.City == city || x.ShipTo.Country == country)
                    .ToList();

                return orders;
            }
        }
    }
}

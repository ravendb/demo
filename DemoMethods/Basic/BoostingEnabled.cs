using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
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

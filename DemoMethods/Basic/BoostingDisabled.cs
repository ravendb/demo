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
        public object BoostingDisabled(string city = "London", string country = "Denmark")
        {
            try
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    var orders = session.Query<Order, OrderByCompanyAndCountry>()
                        .Where(x => x.ShipTo.City == city || x.ShipTo.Country == country)
                        .ToList();

                    return orders;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}

using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object BoostingDisabled()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var orders = session.Query<Order, IndexOrderByCompanyAndCountry>()
                    .Where(x => x.ShipTo.City == "London" || x.ShipTo.Country == "Denmark")
                    .ToList();

                return orders;
            }
        }
    }
}

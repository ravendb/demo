using System.Linq;
using System.Web;
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
                var nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var city = nvc["City"] ?? "London";
                var country = nvc["Country"] ?? "Denmark"; 

                var orders = session.Query<Order, IndexOrderByCompanyAndCountry>()
                    .Where(x => x.ShipTo.City == city || x.ShipTo.Country == country)
                    .ToList();

                return orders;
            }
        }
    }
}

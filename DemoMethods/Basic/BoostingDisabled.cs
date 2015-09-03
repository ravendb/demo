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
        public object BoostingDisabled()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var userParams = new NameValueCollection
                {
                    {"City", "London"},
                    {"Country", "Denmark"}
                };
                DemoUtilities.GetUserParameters(Request.RequestUri.Query, userParams);

                var orders = session.Query<Order, IndexOrderByCompanyAndCountry>()
                    .Where(x => x.ShipTo.City == userParams["City"] || x.ShipTo.Country == userParams["Country"])
                    .ToList();

                return orders;
            }
        }
    }
}

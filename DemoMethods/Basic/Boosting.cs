using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object Boosting()
        {
            using (var session = Store.OpenSession())
            {
                new Index_OrderByCompanyAndShipper().Execute(Store);

                var orders = session.Query<Order,Index_OrderByCompanyAndShipper>()
                .ToList();

                return DemoUtilities.Instance.ObjectToJson(orders);
            }
        }
    }
}

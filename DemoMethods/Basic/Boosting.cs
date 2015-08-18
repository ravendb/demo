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
                new IndexOrderByCompanyAndShipper().Execute(Store);

                var orders = session.Query<Order,IndexOrderByCompanyAndShipper>()
                .ToList();

                return DemoUtilities.Instance.ObjectToJson(orders);
            }
        }
    }
}

using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        //TODO: Boosting from query
        //TODO: Show explain
        [HttpGet]
        public object Boosting()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                //TODO: have something here, actually do search, for multipel results
                // show the Temp-Index-Score that means something

                var orders = session.Query<Order,IndexOrderByCompanyAndShipper>()
                .ToList();

                return orders;
            }
        }
    }
}

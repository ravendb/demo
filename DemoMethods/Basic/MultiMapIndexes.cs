using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object MultiMapIndexingQuery()
        {
            Store.ExecuteIndex(new Index_NameAndCountry());

            using (var session = Store.OpenSession())
            {

                return session.Query<Index_NameAndCountry.Result, Index_NameAndCountry>()
                    .Customize(x => x.WaitForNonStaleResults())
                    .Search(x => x.Country, "USA")
                    .Select(x => x.Name)
                    .ToList();
            }
        }
    }
}
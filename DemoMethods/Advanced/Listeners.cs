using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Client;
using Raven.Client.Listeners;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        public class UsaOnlyQueryListener : IDocumentQueryListener
        {
            public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
            {
                var documentQuery = queryCustomization as IDocumentQuery<IndexNameAndCountry.Result>;
                if (documentQuery != null)
                    documentQuery.WhereEquals(x => x.Country, "USA");
            }
        }

        [HttpGet]
        public object Listeners()
        {
            //TODO: need to _remove_ this afterward, otherwise posions rests
             DocumentStoreHolder.Store.Listeners.RegisterListener(new UsaOnlyQueryListener());


            using (var session = DocumentStoreHolder.Store.OpenSession())
            {

                var result = session.Query<IndexNameAndCountry.Result, IndexNameAndCountry>();
                //TODO: add support for querying with something else here
                return (result.ToList());
            }
        }
    }
}

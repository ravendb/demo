using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
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
            DocumentStoreHolder.Store.Listeners.RegisterListener(new UsaOnlyQueryListener());

            List<IndexNameAndCountry.Result> results;
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {

                var query = session.Query<IndexNameAndCountry.Result, IndexNameAndCountry>() as IQueryable<IndexNameAndCountry.Result>;

                var nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var name = nvc["name"];
                if (string.IsNullOrEmpty(name) == false)
                {
                    query = query.Where(x => x.Name.StartsWith(name));
                }

                results = query.ToList();

            }
            DocumentStoreHolder.Store.Listeners.QueryListeners = new IDocumentQueryListener[0];

            return results;
        }
    }
}

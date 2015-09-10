using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
                var documentQuery = queryCustomization as IDocumentQuery<NameAndCountry.Result>;
                if (documentQuery != null)
                    documentQuery.WhereEquals(x => x.Country, "USA");
            }
        }

        [HttpGet]
        public object Listeners(string name = null)
        {
            DocumentStoreHolder.Store.Listeners.RegisterListener(new UsaOnlyQueryListener());

            List<NameAndCountry.Result> results;
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var query =
                    session.Query<NameAndCountry.Result, NameAndCountry>() as
                        IQueryable<NameAndCountry.Result>;

                if (string.IsNullOrEmpty(name) == false)
                {
                    query = query.Where(x => x.Name.StartsWith(name));
                }

                results = query.ToList();
            }
            // Unregister Listener : (This is only for demo purpose. Listener shouldn't be unregister)
            DocumentStoreHolder.Store.Listeners.QueryListeners = new IDocumentQueryListener[0];

            return results;
        }
    }
}

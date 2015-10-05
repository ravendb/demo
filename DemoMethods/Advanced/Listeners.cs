using System;
using System.Collections.Generic;
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
                var documentQuery = queryCustomization as IDocumentQuery<Company>;
                if (documentQuery != null)
                    documentQuery.WhereEquals(x => x.Address.Country, "USA");
            }
        }

        [HttpGet]
        public object Listeners(string eid = null)
        {
            // Register Listener : (This is only for demo purpose. Listener shouldn't be registered in this manner)
            DocumentStoreHolder.Store.Listeners.RegisterListener(new UsaOnlyQueryListener());
            try
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    var query = session.Query<Company>();

                    if (string.IsNullOrEmpty(eid) == false)
                    {
                        return query.Where(x => x.ExternalId == eid).ToList();
                    }

                    return query.ToList();
                }
            }
            finally
            {
                // Unregister Listener : (This is only for demo purpose. Listener shouldn't be unregister)
                DocumentStoreHolder.Store.Listeners.QueryListeners = new IDocumentQueryListener[0];
                  
            }
           
        }
    }
}

using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        //public class UsaOnlyQueryListener : IDocumentQueryListener
        //{
        //    public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
        //    {
        //        var documentQuery = queryCustomization as IDocumentQuery<Company>;
        //        documentQuery?.WhereEquals(x => x.Address.Country, "USA");
        //    }
        //}

        //[HttpGet]
        //[Route("/advanced/listeners")]
        //[Demo("Listeners", DemoOutputType.Flatten, demoOrder: 250)]
        public object Listeners(string eid = null)
        {
            // Register Listener : (This is only for demo purpose. Listener shouldn't be registered in this manner)
            //DocumentStoreHolder.Store.Listeners.RegisterListener(new UsaOnlyQueryListener());
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
                //DocumentStoreHolder.Store.Listeners.QueryListeners = new IDocumentQueryListener[0];
            }
        }
    }
}

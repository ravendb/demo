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
        public class QueryListener : IDocumentQueryListener
        {
            public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
            {
                queryCustomization.NoCaching();
                queryCustomization.WaitForNonStaleResults();
            }
        }

        [HttpGet]
        public object Listeners()
        {
            // make sure we create new stale index
            DocumentStoreHolder.Store.DatabaseCommands.DeleteIndex("Index/NewIndex/Product");

            DocumentStoreHolder.Store.Listeners.RegisterListener(new QueryListener());

            DocumentStoreHolder.Store.ExecuteIndex(new IndexNewIndexProduct());

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                
                var result = session.Query<Product>("Index/NewIndex/Product");
                return DemoUtilities.Instance.ObjectToJson(result.ToList());
            }
        }
    }
}

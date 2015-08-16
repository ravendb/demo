using Raven.Client;
using System.Web.Http;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        public static IDocumentStore Store = new DocumentStoreHolder().Store;

        [HttpGet]
        public object Advanced()
        {
            return null;
        }
    }
}

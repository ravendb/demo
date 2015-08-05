using Raven.Client;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        public static IDocumentStore Store = new DocumentStoreHolder().Store;

        [HttpGet]
        public object Basic()
        {
            return null;
        }
    }
}

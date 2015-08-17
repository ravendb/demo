using System.Web.Http;
using Raven.Client;

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

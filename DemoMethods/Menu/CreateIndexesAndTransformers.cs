using System.Reflection;
using System.Web.Http;
using Raven.Client.Indexes;

namespace DemoMethods
{
    public partial class MenuController : ApiController
    {
        [HttpGet]
        public object CreateIndexesAndTransformers()
        {
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), DocumentStoreHolder.Store);
            return ("Indexes were created successfully");
        }
    }
}
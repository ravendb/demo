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
            // Side By Side Index Creation:
            // IndexCreation.SideBySideCreateIndexes(Assembly.GetExecutingAssembly(), DocumentStoreHolder.Store);
            
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), DocumentStoreHolder.Store);



            return ("Indexes, Transformers and Facets were created successfully");
        }
    }
}
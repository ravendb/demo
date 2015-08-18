using System.Web.Http;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        // TODO :: implement.. 
        [HttpGet]
        public object FullTextSearch()
        {            
            // new Index_Category().Execute(Store);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
               

                return DemoUtilities.Instance.ObjectToJson(null);
            }
        }
    }
}

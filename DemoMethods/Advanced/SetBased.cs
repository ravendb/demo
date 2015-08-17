using System.Web.Http;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object SetBased()
        {
            
                return DemoUtilities.Instance.ObjectToJson(null);
            
        }
    }
}

using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace DemoMethods
{
    public class MenuController : ApiController
    {
        [HttpGet]        
        public object Index()
        {
            var allControllerTypes = from type in GetType().Assembly.GetTypes()
                                     where type.BaseType.FullName.Contains("ApiController")
                                     select type;

            var allPublicMethods = allControllerTypes.SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                                                     .Where(x => x.CustomAttributes.Any(attr => attr.AttributeType == typeof(HttpGetAttribute)))
                                                     .Select(x => $"{x.DeclaringType.FullName}::{x.Name}");

            return DemoUtilities.ObjectToJson(allPublicMethods);
        }
    }
}

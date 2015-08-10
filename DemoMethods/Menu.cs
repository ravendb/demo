using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace DemoMethods
{
    public class MenuController : ApiController
    {
        public class FormattedMenuIndex
        {
            public static object Format(List<string> list)
            {
                for (int i = 0; i < list.Count; i++)
                    list[i] = string.Format("{0}/{1}", DemoUtilities.ServerInfo, list[i].Replace("Controller", ""));
                return DemoUtilities.Instance.ObjectToJson(list);
            }
        }

        [HttpGet]        
        public object Index()
        {
            var allControllerTypes = from type in GetType().Assembly.GetTypes()
                                     where type.BaseType.FullName.Contains("ApiController")
                                     select type;

            var allPublicMethods = allControllerTypes.SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                                                     .Where(x => x.CustomAttributes.Any(attr => attr.AttributeType == typeof(HttpGetAttribute)))
                                                     .Select(x => $"{x.DeclaringType.Name}/{x.Name}");


            return FormattedMenuIndex.Format((allPublicMethods.ToList()));
        }
    }
}

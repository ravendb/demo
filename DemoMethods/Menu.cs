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
                List<string> resultList = new List<string>();
                foreach (var i in list)
                {
                    string s = i.Replace("Controller", "");
                    Console.WriteLine("{0}/{1}", DemoUtilities.ServerInfo, s);
                    // TODO :: formated json
                }
                return list;
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


            return DemoUtilities.Instance.ObjectToJson(FormattedMenuIndex.Format(allPublicMethods.ToList()));
        }
    }
}

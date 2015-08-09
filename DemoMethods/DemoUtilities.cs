using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Http;
using System.Web.Mvc;

namespace DemoMethods
{
    public class DemoUtilities : Controller
    {
        private static volatile DemoUtilities instance = null;
        private static object lockObj = new object();
        private DemoUtilities() { }

        public static DemoUtilities Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new DemoUtilities();
                        }
                    }
                }
                return instance;
            }
        }


        public object ObjectToJson(object Obj)
        {
            /*
            var s = JsonConvert.SerializeObject(Obj, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); ;  // TODO :: Json(Obj)   
            */
            return Json(Obj);
        }

        public static string ServerInfo { get; set; }
    }
}

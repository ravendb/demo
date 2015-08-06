using Newtonsoft.Json;

namespace DemoMethods
{
    public class DemoUtilities
    {
        public static object ObjectToJson(object Obj)
        {
            return JsonConvert.SerializeObject(Obj, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });;  // TODO :: Json(Obj)   
        }

        public static string ServerInfo { get; set; }
    }
}

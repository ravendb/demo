using System.Web.Mvc;

namespace DemoMethods
{
    public class DemoUtilities : Controller
    {
        private static volatile DemoUtilities instance = null;
        private static readonly object LockObj = new object();
        private DemoUtilities() { }

        public static DemoUtilities Instance
        {
            get
            {
                if (instance != null) return instance;
                lock(LockObj)
                {
                    if (instance == null)
                    {
                        instance = new DemoUtilities();
                    }
                }
                return instance;
            }
        }


        public object ObjectToJson(object obj)
        {
            return Json(obj);
        }

        public static string ServerInfo { get; set; }
    }
}

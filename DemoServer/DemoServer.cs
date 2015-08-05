using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace DemoServer
{
    public class DemoServer
    {
        public void Start(string Url, int Port)
        {
            var config = new HttpSelfHostConfiguration(string.Format("http://{0}:{1}", Url, Port));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}", // {*url}", 
                defaults: new { id = RouteParameter.Optional } 
                );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
    
            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@" __   ___        __      __   ___  __        ___  __  ");
                Console.WriteLine(@"|  \ |__   |\/| /  \    /__` |__  |__) \  / |__  |__)");
                Console.WriteLine(@"|__/ |___  |  | \__/    .__/ |___ |  \  \/  |___ |  \ ");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("              http://{0}:{1}", Url, Port);
                Console.WriteLine("              Press any key to stop ...");
                Console.ResetColor();
                 
                Console.ReadKey();
            }
        }
    }

}
using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.SelfHost;
using DemoMethods;
using DemoStudio;


namespace DemoServer
{
    public class DemoServer
    {
        public void Start(string Url, int Port)
        {
            DemoUtilities.ServerInfo = string.Format("http://{0}:{1}", Url, Port);
            var config = new HttpSelfHostConfiguration(DemoUtilities.ServerInfo);        
            var init = new DemoStudioInit();
            config.Routes.MapHttpRoute(
                name: "StudioScripts",
                routeTemplate: "studio/scripts/{*path}",
                defaults: new { controller = "DemoStudio", action = "LoadScript"}
                );
            config.Routes.MapHttpRoute(
                name: "StudioFile",
                routeTemplate: "",
                defaults: new { controller = "DemoStudio", action = "GetStudioFile", id = RouteParameter.Optional }
                );
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{*id}", // {*url}", 
                // defaults: new {controller = "Menu", action = "Index", id = RouteParameter.Optional}
                defaults: new { controller = "DemoStudio", action = "GetStudioFile", id = RouteParameter.Optional }
                );
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@" __   ___        __      __   ___  __        ___  __  ");
                Console.WriteLine(@"|  \ |__   |\/| /  \    /__` |__  |__) \  / |__  |__) ");
                Console.WriteLine(@"|__/ |___  |  | \__/    .__/ |___ |  \  \/  |___ |  \ ");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("              http://{0}:{1}", Url, Port);
                Console.WriteLine("              Press any key to stop ...");
                Console.ResetColor();

                Process.Start(DemoUtilities.ServerInfo);

                Console.ReadKey();
            }
        }
    }
}
    
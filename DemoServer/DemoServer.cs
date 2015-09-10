using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.SelfHost;
using DemoMethods;


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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(@"                                             Rel: 0.1");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("      Demo Serving  @ http://{0}:{1}", Url, Port);
                Console.WriteLine("      Using RavenDB @ http://{0}:{1}", DocumentStoreHolder.Address, DocumentStoreHolder.Port);
                Console.WriteLine("      For Database  : {0}", DocumentStoreHolder.DatabaseName);
                Console.WriteLine("");                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("              Press any key to stop ...");
                Console.ResetColor();

                Process.Start(DemoUtilities.ServerInfo);

                Console.ReadKey();
            }
        }
    }
}
    
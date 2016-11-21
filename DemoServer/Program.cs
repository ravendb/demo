using System;
using System.IO;
using System.Threading.Tasks;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace DemoServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var demoServerUrl = "localhost:9191";
            if (args.Length > 0)
            {
                if (args[0] != null)
                {
                    switch (args[0])
                    {
                        case "-h":
                        case "-help":
                        case "--help":
                        case "/h":
                        case "/?":
                            {
                                Console.WriteLine(@"");
                                Console.WriteLine(@"DemoServer Usage : demoserver [-h] [-ds address] [-dn databaseName]");
                                Console.WriteLine(@"");
                                Console.WriteLine(@"    -ds address : Demo Server's address.");
                                Console.WriteLine(@"                  Default : 'localhost:9090'");
                                Console.WriteLine(@"");
                                Console.WriteLine(@"Note : Do not add 'http://' prefix to addresses.");
                                Console.WriteLine(@"");
                            }
                            return;
                        default:
                            {
                                var readDs = false;
                                for (int i = 0; i < args.Length; i++)
                                {
                                    if (args[i] != null)
                                    {
                                        if (readDs)
                                        {
                                            demoServerUrl = args[i];
                                            readDs = false;
                                            continue;
                                        }

                                        if (args[i].Equals("-ds"))
                                            readDs = true;
                                        else
                                        {
                                            Console.WriteLine("");
                                            Console.WriteLine("Error : Bad usage parameters. Use -h for help.");
                                            Console.WriteLine("");
                                            return;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            demoServerUrl = string.Format("http://{0}", demoServerUrl);
            var dsUri = new Uri(demoServerUrl);

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(demoServerUrl)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@" __   ___        __      __   ___  __        ___  __  ");
            Console.WriteLine(@"|  \ |__   |\/| /  \    /__` |__  |__) \  / |__  |__) ");
            Console.WriteLine(@"|__/ |___  |  | \__/    .__/ |___ |  \  \/  |___ |  \ ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(@"                                             Rel: 0.1");
            Console.WriteLine(string.Empty);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("      Demo Serving  @ http://{0}:{1}", dsUri.Host, dsUri.Port);
            Console.WriteLine("      Using RavenDB @ {0}", DocumentStoreHolder.ServerUrl);
            Console.WriteLine("      Demo Database   : {0}", DocumentStoreHolder.NorthwindDatabaseName);
            Console.WriteLine("      Media Database  : {0}", DocumentStoreHolder.MediaDatabaseName);
            Console.WriteLine(string.Empty);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.ResetColor();

            host.Run();
        }
    }
}

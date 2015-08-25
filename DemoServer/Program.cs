using System;
using DemoMethods;

namespace DemoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // string demoServerUrl = "localhost.fiddler:9090";
            // string ravenServerUrl = "localhost.fiddler:8080";
            string demoServerUrl = "localhost:9090";
            string ravenServerUrl = "localhost:8080";
            string databaseName = "Northwind";
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
                                Console.WriteLine(@"DemoServer Usage : demoserver [-h] [-ds address] [-rs address] [-dn databaseName]");
                                Console.WriteLine(@"");
                                Console.WriteLine(@"    -ds address : Demo Server's address.");
                                Console.WriteLine(@"                  Default : 'localhost:9090'");
                                Console.WriteLine(@"");
                                Console.WriteLine(@"    -rs address : RavenDB Server's address.");
                                Console.WriteLine(@"                  Default : 'localhost:8080'");
                                Console.WriteLine(@"");
                                Console.WriteLine(@"    -dn address : Database Name");
                                Console.WriteLine(@"                  Default : 'Northwind'");
                                Console.WriteLine(@"");
                                Console.WriteLine(@"Note : Do not add 'http://' prefix to addresses.");
                                Console.WriteLine(@"");                                
                            }
                            return;
                        default:
                            {
                                bool readDs = false;
                                bool readRs = false;
                                bool readDn = false;
                                for (int i = 0; i < args.Length; i++)
                                {
                                    if (args[i] != null)
                                    {
                                        if (readDs == true)
                                        {
                                            demoServerUrl = args[i];
                                            readDs = false;
                                            continue;
                                        }
                                        if (readRs == true)
                                        {
                                            ravenServerUrl = args[i];
                                            readRs = false;
                                            continue;
                                        }
                                        if (readDn == true)
                                        {
                                            databaseName = args[i];
                                            readDn = false;
                                            continue;
                                        }

                                        if (args[i].Equals("-ds"))
                                            readDs = true;
                                        else if (args[i].Equals("-rs"))
                                            readRs = true;
                                        else if (args[i].Equals("-dn"))
                                            readDn = true;
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
            demoServerUrl = string.Format("Http://{0}", demoServerUrl);
            ravenServerUrl = string.Format("Http://{0}", ravenServerUrl);
            var dsUri = new Uri(demoServerUrl);
            var rsUri = new Uri(ravenServerUrl);
            DocumentStoreHolder.SetDbInfo(rsUri.Host, rsUri.Port, databaseName);
            var server = new DemoServer();
            server.Start(dsUri.Host, dsUri.Port);
        }
    }
}

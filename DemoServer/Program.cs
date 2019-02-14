using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace DemoServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(x => x.AddNLog())
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddJsonFile("appsettings.json", true)
                        .AddEnvironmentVariables("RAVENDEMO_");
                })
                .UseStartup<Startup>();
    }
}

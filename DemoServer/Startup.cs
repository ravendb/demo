using System.Collections.Generic;
using System.IO;
using DemoCommon.Utils.Database;
using DemoParser.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.Demos;
using DemoServer.Utils.Filters;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoServer
{
    public class Startup
    {
        private static readonly Dictionary<DemoLanguage, string> DemoCodePaths = new Dictionary<DemoLanguage, string>
        {
            {DemoLanguage.CSharp, Path.Combine("Controllers", "Demos")},
            {DemoLanguage.Java, Path.Combine("AdditionalLanguages", "java", "src", "main", "java", "net", "ravendb", "demo")},
            {DemoLanguage.NodeJs, Path.Combine("AdditionalLanguages", "nodejs", "demo")},
            {DemoLanguage.Go, Path.Combine("AdditionalLanguages", "go")}
            // add path
        };

        public Startup(IConfiguration configuration, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            HostingEnvironment = env;
            LoggerFactory = loggerFactory;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment HostingEnvironment { get; }
        private ILoggerFactory LoggerFactory { get; }

        private string GetSpaOutputDir(IWebHostEnvironment env) => env.IsDevelopment() ? "wwwroot/dev" : "wwwroot/dist";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            var settings = new Settings();
            Configuration.Bind(settings);

            services.AddSingleton<Settings>(settings);

            services.AddMemoryCache();

            services.AddMvc();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = GetSpaOutputDir(HostingEnvironment);
            });

            var demoContainer = DemoContainer.Initialize(DemoCodePaths, LoggerFactory.CreateLogger<DemoContainer>(), settings);
            
            services.AddSingleton(demoContainer);
            services.AddSingleton<DocumentStoreHolder>();
            services.AddSingleton(_ => new DatabaseName(settings.Database, settings.ConferenceMode));

            services.AddScoped<UserStoreCache>();
            services.AddScoped<MediaStoreCache>();

            services.AddScoped<HeadersAccessor>();
            services.AddScoped<UserIdContainer>();
            services.AddScoped<DatabaseSetup>();
            services.AddScoped<DatabaseLinks>();
            services.AddScoped<AddUserIdToResponseHeaderAttribute>();
            
            services
                .AddControllers()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddControllersAsServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = env.ContentRootPath;
                    spa.UseReactDevelopmentServer("dev-webpack");
                }
                
                spa.Options.SourcePath = GetSpaOutputDir(env);
            });
        }
    }
}

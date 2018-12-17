using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DemoServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            HostingEnvironment = env;
            LoggerFactory = loggerFactory;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment { get; }
        private ILoggerFactory LoggerFactory { get; }

        private string GetSpaOutputDir(IHostingEnvironment env) => env.IsDevelopment() ? "wwwroot/dev" : "wwwroot/dist";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            var settings = new Settings();
            Configuration.Bind(settings);

            services.AddSingleton<Settings>(settings);

            var serviceProvider = services.BuildServiceProvider();
            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = GetSpaOutputDir(HostingEnvironment);
            });

            var demoContainer = DemoContainer.Initialize("Controllers\\Demos", LoggerFactory.CreateLogger<DemoContainer>());
            services.AddSingleton(demoContainer);
            services.AddSingleton<DocumentStoreHolder>();

            services.AddScoped<DocumentStoreCache>();
            services.AddScoped<HeadersAccessor>();
            services.AddScoped<DatabaseSetup>();
            services.AddScoped<DatabaseLinks>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = GetSpaOutputDir(env);
            });
        }
    }
}

namespace DemoServer.Web
{
    using System.Diagnostics;
    using System.Web.Http;

    using DemoMethods;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DocumentStoreHolder.SetDbInfo("DemoServer", "Northwind", "Media");

            var configuration = new HttpConfiguration();
            DemoServer.Configure(configuration);

            app.UseWebApi(configuration);
        }
    }
}
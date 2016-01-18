using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DemoMethods.Helpers
{
    using System.IO;
    using System.Web.Hosting;

    public class DemoApiController : ApiController
    {
        private string basePath;
        protected string BasePath
        {
            get
            {
                if (string.IsNullOrEmpty(basePath))
                {
                    basePath = HostingEnvironment.MapPath("~/");
                    if (string.IsNullOrEmpty(basePath) == false)
                    {
                        basePath = Path.GetFullPath(basePath + "..\\");
                    }
                    else
                    {
                        basePath = Path.GetFullPath("..\\..\\..\\");
                    }
                }

                return basePath;
            }
        }

        protected TimeSpan? ServerTime { get; set; }

        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();
            var message = await base.ExecuteAsync(controllerContext, cancellationToken).ConfigureAwait(false);

            AddTime("Client-Time", message, watch.Elapsed);

            if (ServerTime.HasValue)
                AddTime("Server-Time", message, ServerTime.Value);

            return message;
        }

        private static void AddTime(string name, HttpResponseMessage message, TimeSpan time)
        {
            var timeRounded = Math.Round(time.TotalSeconds, 2, MidpointRounding.ToEven);

            message.Headers.TryAddWithoutValidation(name, timeRounded.ToString(CultureInfo.InvariantCulture));
        }
    }
}
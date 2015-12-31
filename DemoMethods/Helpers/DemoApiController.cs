using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DemoMethods.Helpers
{
    public class DemoApiController : ApiController
    {
        protected TimeSpan? ServerTime { get; set; }

        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();
            var message = await base.ExecuteAsync(controllerContext, cancellationToken).ConfigureAwait(false);

            AddTime("Demo-Time", message, watch.Elapsed);

            if (ServerTime.HasValue)
                AddTime("Server-Time", message, ServerTime.Value);

            return message;
        }

        private static void AddTime(string name, HttpResponseMessage message, TimeSpan time)
        {
            message.Headers.TryAddWithoutValidation(name, ((int)time.TotalMilliseconds).ToString());
        }
    }
}
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers
{
    public class BaseController : Controller
    {
        private string _basePath;
        protected string BasePath
        {
            get
            {
                if (string.IsNullOrEmpty(_basePath))
                {
                    var env = (IHostingEnvironment)HttpContext.RequestServices.GetService(typeof(IHostingEnvironment));
                    _basePath = env.ContentRootPath;
                    _basePath = string.IsNullOrEmpty(_basePath) == false 
                        ? _basePath 
                        : Path.GetFullPath("..\\..\\..\\");
                }

                return _basePath;
            }
        }

        protected TimeSpan? ServerTime { get; set; }

        //public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        //{
        //    var watch = Stopwatch.StartNew();
        //    var message = await base.ExecuteAsync(controllerContext, cancellationToken).ConfigureAwait(false);

        //    AddTime("Client-Time", message, watch.Elapsed);

        //    if (ServerTime.HasValue)
        //        AddTime("Server-Time", message, ServerTime.Value);

        //    return message;
        //}

        private static void AddTime(string name, HttpResponseMessage message, TimeSpan time)
        {
            var timeRounded = Math.Round(time.TotalSeconds, 2, MidpointRounding.ToEven);

            message.Headers.TryAddWithoutValidation(name, timeRounded.ToString(CultureInfo.InvariantCulture));
        }
    }
}
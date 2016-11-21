using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

        private Stopwatch _watch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _watch = Stopwatch.StartNew();
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            AddTime("Client-Time", Response, _watch.Elapsed);

            if (ServerTime.HasValue)
                AddTime("Server-Time", Response, ServerTime.Value);
        }

        private static void AddTime(string name, HttpResponse response, TimeSpan time)
        {
            var timeRounded = Math.Round(time.TotalSeconds, 2, MidpointRounding.ToEven);

            response.Headers.Add(name, timeRounded.ToString(CultureInfo.InvariantCulture));
        }
    }
}
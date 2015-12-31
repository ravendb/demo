using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using DemoMethods.Helpers;
using Raven.Client.Connection;

namespace DemoMethods
{
    // TODO:: remove this and load dll forcibly on server startup
    public class DemoStudioInit
    {
        public string UrlString { get; set; }
    }

    public class DemoStudioController : DemoApiController
    {
        [HttpGet]
        public HttpResponseMessage LoadScript(string path)
        {
            return WriteEmbeddedFile("Scripts/" + path);
        }

        [HttpGet]
        public HttpResponseMessage GetStudioFile(string path = "index.html")
        {
            return WriteEmbeddedFile(path);
        }

        public HttpResponseMessage WriteEmbeddedFile(string docPath)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../DemoStudio", docPath);
            if (File.Exists(filePath))
                return WriteFile(filePath);

            filePath = Path.Combine("../../../DemoStudio", docPath);
            if (File.Exists(filePath))
                return WriteFile(filePath);
            return null;
        }

        public HttpResponseMessage WriteFile(string filePath)
        {

            var msg = new HttpResponseMessage
            {
                Content =
                    new CompressedStreamContent(
                        new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), false)
            };

            var type = GetContentType(filePath);
            msg.Content.Headers.ContentType = new MediaTypeHeaderValue(type);

            return msg;
        }

        private static string GetContentType(string docPath)
        {
            switch (Path.GetExtension(docPath))
            {
                case ".html":
                case ".htm":
                    return "text/html";
                case ".css":
                    return "text/css";
                case ".js":
                    return "text/javascript";
                case ".ico":
                    return "image/vnd.microsoft.icon";
                case ".jpg":
                    return "image/jpeg";
                case ".gif":
                    return "image/gif";
                case ".png":
                    return "image/png";
                case ".xap":
                    return "application/x-silverlight-2";
                default:
                    return "text/plain";
            }
        }
    }
}

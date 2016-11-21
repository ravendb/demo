using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers
{
    public class StudioController : BaseController
    {
        [HttpGet]
        [Route("/studio/scripts/{*path}")]
        public FileStreamResult LoadScript(string path)
        {
            return WriteEmbeddedFile("Scripts/" + path);
        }

        [HttpGet]
        [Route("/")]
        public FileStreamResult GetStudioFile(string path = "index.html")
        {
            return WriteEmbeddedFile(path);
        }

        private FileStreamResult WriteEmbeddedFile(string docPath)
        {
            var filePath = Path.GetFullPath(BasePath + docPath);
            if (System.IO.File.Exists(filePath))
                return WriteFile(filePath);

            return null;
        }

        private static FileStreamResult WriteFile(string filePath)
        {
            return new FileStreamResult(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), GetContentType(filePath));
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
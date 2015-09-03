using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Raven.Abstractions.Data;
using Raven.Abstractions.Extensions;
using Raven.Json.Linq;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public async Task<object> UploadDownloadFile()
        {           
            // Create File DemoFs.txt 
            var filename = Path.GetTempPath() + "DemoFs.txt";
            const string storeString = "Hello World";
            File.WriteAllText(filename, storeString, Encoding.UTF8);

            // Upload
            var fs = File.OpenRead(filename);
            await FileStoreHolder.FilesSystemStore.AsyncFilesCommands.UploadAsync("/demofile.txt", fs,
               new RavenJObject 
                                {
                                    {
                                        "AllowRead", "Everyone"
                                    } 
                                });
            fs.Close();

            // Download
            var metadata = new Reference<RavenJObject>();
            string content;

            using (var stream = await FileStoreHolder.FilesSystemStore.AsyncFilesCommands.DownloadAsync("/demofile.txt", metadata))
            {
                var size = metadata.Value[Constants.FileSystem.RavenFsSize];
                var bufferLength = size.Value<int>();
                byte[] buffer = new byte[bufferLength];
                stream.Read(buffer, 0, bufferLength);
                content = Encoding.UTF8.GetString(buffer);
            }

            var results = new
            {
                Read = content,
                MetaData = metadata
            };

            return (results);
        }
    }
}


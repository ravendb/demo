using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Raven.Abstractions.Data;
using Raven.Abstractions.Extensions;
using Raven.Client.FileSystem;
using Raven.Json.Linq;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public async Task<object> UploadDownloadFile()
        {
            using (IFilesStore filesStore = new FilesStore()
            {
                Url = String.Format("http://{0}:{1}", DocumentStoreHolder.Address, DocumentStoreHolder.Port),
                DefaultFileSystem = DocumentStoreHolder.DatabaseName + "FS"
            }.Initialize())
            {
                string filename = Path.GetTempPath() + "DemoFs.txt";
                string storeString = "Hello World";

                File.WriteAllText(filename, storeString, Encoding.UTF8);

                await filesStore.AsyncFilesCommands.UploadAsync("/demofile.txt", File.OpenRead(filename),
                   new RavenJObject 
                                {
                                    {
                                        "AllowRead", "Everyone"
                                    } 
                                });

                var metadata = new Reference<RavenJObject>();


                string content;
                using (var stream = await filesStore.AsyncFilesCommands.DownloadAsync("/demofile.txt", metadata))
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

                return DemoUtilities.Instance.ObjectToJson(results);
            }
        }
    }
}

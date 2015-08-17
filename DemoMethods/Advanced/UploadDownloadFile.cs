using DemoMethods.Entities;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.FileSystem;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Raven.Abstractions.Data;
using Raven.Client.Bundles.MoreLikeThis;
using Raven.Client.Document;
using Raven.Abstractions.Indexing;
using Raven.Database;
using System.IO;
using Raven.Json.Linq;
using System.Threading.Tasks;
using Raven.Abstractions.Extensions;
using System.Text;
using Raven.Client.Linq.Indexing;

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
                    content = Encoding.UTF8.GetString(buffer);.
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

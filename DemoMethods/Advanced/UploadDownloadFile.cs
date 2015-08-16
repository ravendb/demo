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

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object UploadDownloadFile()
        {
            using (IFilesStore filesStore = new FilesStore()
            {
                Url = String.Format("http://{0}:{1}", DocumentStoreHolder.Address, DocumentStoreHolder.Port),
                DefaultFileSystem = DocumentStoreHolder.DatabaseName + "FS"
            }.Initialize())
            {
                string filename = Path.GetTempPath() + "DemoFs.txt";

                File.WriteAllText(filename, "Hello World", Encoding.UTF8);
                

                var taskUpload = Task.Factory.StartNew(new Func<Task>(async () =>
                    {
                        await filesStore.AsyncFilesCommands.UploadAsync(
                            "/demofile.txt",
                            File.OpenRead(filename),
                            new RavenJObject 
                                {
                                    {
                                        "AllowRead", "Everyone"
                                    } 
                                });
                    }));

                taskUpload.Wait();

                var metadata = new Reference<RavenJObject>();

                var taskDownload = Task.Factory.StartNew(new Func<Task>(async () =>
                    {
                        Stream data = await filesStore
                            .AsyncFilesCommands
                            .DownloadAsync(
                            "/demofile.txt",
                            metadata);

                        // VolatileStringResult = data.ReadString(Encoding.UTF8);
                        byte[] res = new byte[11];
                        data.Read(res, 0, 11);
                        Console.WriteLine("DEBUG:{0}", res);
                    }));

                taskDownload.Wait();
            }
           
            // return DemoUtilities.Instance.ObjectToJson(VolatileStringResult);
            return null;
        }
    }
}

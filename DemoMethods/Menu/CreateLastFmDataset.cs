using System.IO;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using Raven.Abstractions.Data;
using Raven.Imports.Newtonsoft.Json;

namespace DemoMethods
{
    public partial class MenuController : ApiController
    {
        public static int LastFMKey;
        [HttpGet]
        public object CreateLastFmDataset()
        {
            // var path = Path.GetFullPath("../../../DemoMethods/lastfm_subset");
            var path = Path.GetFullPath("../../../../lastfm_train");
            var di = new DirectoryInfo(path);

            LastFMKey = 1;

            return AddDocumentsToDb(di) ? (string.Format("Last FM Dataset was added to {0} database", DocumentStoreHolder.DatabaseName)) 
                : (string.Format("ERROR : Unable to add Last FM Dataset to {0} database", DocumentStoreHolder.DatabaseName));
        }

        public bool AddDocumentsToDb(DirectoryInfo root)
        {
            FileInfo[] files;

            try
            {
                files = root.GetFiles("*.*");
            }
            catch 
            {
                return false;
            }
            
            using (var bulkInsert = DocumentStoreHolder.Store.BulkInsert(options: new BulkInsertOptions {OverwriteExisting = true, BatchSize = 256}))
            {
                foreach (var fi in files)
                {
                    var key = string.Format("LastFM/{0}", LastFMKey++);
                    var jsonString = File.ReadAllText(fi.FullName);
                    var deserializedObject = JsonConvert.DeserializeObject<LastFm>(jsonString);

                    // Instead of : DocumentStoreHolder.Store.DatabaseCommands.Put(key, null, jsonObj, null);
                    // Perform Bulk Insert :
                    bulkInsert.Store(deserializedObject, key);                    
                }
            }

            var subDirs = root.GetDirectories();
            return subDirs.All(dirInfo => AddDocumentsToDb(dirInfo));
        }        
    }
}
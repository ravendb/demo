using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using Raven.Abstractions.Data;
using Raven.Imports.Newtonsoft.Json;
using Raven.Json.Linq;

namespace DemoMethods
{
    public partial class MenuController : ApiController
    {
        [HttpGet]
        public object CreateLastFmDataset(string path = @"C:\Users\adi\Downloads\lastfm_subset.zip")
        {
            // path = @"C:\Users\adi\Downloads\lastfm_train.zip";
            try
            {
                AddDocumentsToDb(path);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return string.Format("Last FM Dataset was added to {0} database", DocumentStoreHolder.DatabaseName);
        }

        public void AddDocumentsToDb(string path)
        {
            int count = 1;
            using (var stream = File.OpenRead(path))
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
            using (var bulkInsert = DocumentStoreHolder.Store.BulkInsert(options: new BulkInsertOptions { OverwriteExisting = true, BatchSize = 256 }))
            {
                foreach (var entry in zip.Entries)
                {
                    if (entry.Length == 0)
                        continue;
                    using (var entryStream = entry.Open())
                    {
                        var docAsJson = RavenJObject.Load(new JsonTextReader(new StreamReader(entryStream)));
                        var doc = new LastFm
                        {
                            Artist = docAsJson.Value<string>("artist"),
                            TimeStamp = DateTime.Parse(docAsJson.Value<string>("timestamp")),
                            Title = docAsJson.Value<string>("title"),
                            TrackId = docAsJson.Value<string>("track_id"),
                            Tags =
                                docAsJson.Value<RavenJArray>("tags")
                                    .Select(x => ((RavenJArray)x)[0].Value<string>())
                                    .ToList()
                        };
                        bulkInsert.Store(doc, "lastfm/" + (count++));
                    }
                }
            }
        }
    }
}
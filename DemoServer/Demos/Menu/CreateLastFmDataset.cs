using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Raven.Client.Server;
using Raven.Client.Server.Operations;

namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        [HttpGet]
        [Route("/menu/createLastFmDataset")]
        [Demo("Deploy Last.fm", DemoOutputType.String, demoOrder: 310)]
        public object CreateLastFmDataset(string path = null, bool deleteDatabase = false)
        {
            try
            {
                if (deleteDatabase)
                {
                    DocumentStoreHolder.MediaStore
                        .Admin
                        .Server
                        .Send(new DeleteDatabaseOperation(DocumentStoreHolder.MediaDatabaseName, hardDelete: true));
                }

                DocumentStoreHolder.MediaStore
                    .Admin
                    .Server
                    .Send(new CreateDatabaseOperation(new DatabaseRecord(DocumentStoreHolder.MediaDatabaseName)));

                AddDocumentsToDb(path);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Format("Last.fm was deployed to {0} database.", DocumentStoreHolder.MediaDatabaseName);
        }

        public void AddDocumentsToDb(string path)
        {
            using (var stream = string.IsNullOrWhiteSpace(path) ? GetEmbeddedLastFmSubset() : System.IO.File.OpenRead(path))
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
            using (var bulkInsert = DocumentStoreHolder.MediaStore.BulkInsert())
            {
                foreach (var entry in zip.Entries)
                {
                    if (entry.Length == 0)
                        continue;
                    using (var entryStream = entry.Open())
                    {
                        var docAsJson = JObject.Load(new JsonTextReader(new StreamReader(entryStream)));
                        var doc = new LastFm
                        {
                            Artist = docAsJson.Value<string>("artist"),
                            TimeStamp = DateTime.Parse(docAsJson.Value<string>("timestamp")),
                            Title = docAsJson.Value<string>("title"),
                            TrackId = docAsJson.Value<string>("track_id"),
                            Tags = docAsJson.Value<JArray>("tags")
                                    .Select(x => ((JArray)x)[0].Value<string>())
                                    .ToList()
                        };
                        bulkInsert.Store(doc);
                    }
                }
            }
        }

        private Stream GetEmbeddedLastFmSubset()
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream($"{typeof(Program).Namespace}.Data.lastfm_subset.zip");
        }
    }
}
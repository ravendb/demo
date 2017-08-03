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
using Raven.Client.Documents;
using Raven.Client.Documents.Smuggler;
using Raven.Client.Util;

namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        [HttpGet]
        [Route("/menu/createLastFmDataset")]
        [Demo("Deploy Last.fm", DemoOutputType.String, demoOrder: 310)]
        public object CreateLastFmDataset(string path = "C:\\work\\media.ravendbdump", bool deleteDatabase = false)
        {
            if (deleteDatabase)
            {
                DocumentStoreHolder.MediaStore
                    .Admin
                    .Server
                    .Send(new DeleteDatabaseOperation(DocumentStoreHolder.MediaDatabaseName, hardDelete: true));

                WaitForDeleteToComplete(DocumentStoreHolder.MediaStore, DocumentStoreHolder.MediaDatabaseName);

                DocumentStoreHolder.MediaStore
                    .Admin
                    .Server
                    .Send(new CreateDatabaseOperation(new DatabaseRecord(DocumentStoreHolder.MediaDatabaseName)));

                WaitForOperationToComplete(DocumentStoreHolder.MediaStore, DocumentStoreHolder.MediaDatabaseName);
            }

            AddDocumentsToDb(path);
          
            return string.Format("Last.fm was deployed to {0} database.", DocumentStoreHolder.MediaDatabaseName);
        }

        public static void WaitForOperationToComplete(IDocumentStore store, string dbName)
        {
            DatabaseTopology topology;
            do
            {
                topology = store
                .Admin
                .Server
                .Send(new GetDatabaseTopologyOperation(dbName));
            } while (topology.Members.Count < 1);
        }

        public static void WaitForDeleteToComplete(IDocumentStore store, string dbName)
        {
            DatabaseTopology topology;
            do
            {
                topology = store
                .Admin
                .Server
                .Send(new GetDatabaseTopologyOperation(dbName));
            } while (topology != null);
        }

        public void AddDocumentsToDb(string path)
        {
            using (var stream = string.IsNullOrWhiteSpace(path) ? GetEmbeddedLastFmSubset() : System.IO.File.OpenRead(path))
            {
                var options = new DatabaseSmugglerOptions()
                {
                    Database = DocumentStoreHolder.MediaDatabaseName
                };
                AsyncHelpers.RunSync(() => ((DocumentStore)DocumentStoreHolder.MediaStore).Smuggler.ImportAsync(options, stream));               
            }
        }

        private Stream GetEmbeddedLastFmSubset()
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream($"{typeof(Program).Namespace}.Data.lastfm_subset.zip");
        }
    }
}
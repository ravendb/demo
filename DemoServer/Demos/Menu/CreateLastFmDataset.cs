using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Smuggler;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        [HttpGet]
        [Route("/menu/createLastFmDataset")]
        [Demo("Deploy Last.fm", DemoOutputType.String, demoOrder: 305)]
        public async Task<string> CreateLastFmDataset(string path = null, bool deleteDatabase = true)
        {
            if (deleteDatabase)
            {
                try
                {
                    DocumentStoreHolder.MediaStore
                        .Maintenance
                        .Server
                        .Send(new DeleteDatabasesOperation(DocumentStoreHolder.MediaDatabaseName, hardDelete: true));

                    WaitForDeleteToComplete(DocumentStoreHolder.MediaStore, DocumentStoreHolder.MediaDatabaseName);
                }
                catch (DatabaseDoesNotExistException)
                {
                }
            }

            DocumentStoreHolder.MediaStore
                    .Maintenance
                    .Server
                    .Send(new CreateDatabaseOperation(new DatabaseRecord(DocumentStoreHolder.MediaDatabaseName)));

            WaitForOperationToComplete(DocumentStoreHolder.MediaStore, DocumentStoreHolder.MediaDatabaseName);

            await AddDocumentsToDbAsync(path).ConfigureAwait(false);

            return string.Format("Last.fm was deployed to {0} database.", DocumentStoreHolder.MediaDatabaseName);
        }

        public static void WaitForOperationToComplete(IDocumentStore store, string dbName)
        {
            DatabaseTopology topology;
            do
            {
                topology = store
                .Maintenance
                .Server
                .Send(new GetDatabaseRecordOperation(dbName)).Topology;
            } while (topology.Members.Count < 1);
        }

        public static void WaitForDeleteToComplete(IDocumentStore store, string dbName)
        {
            DatabaseTopology topology;
            do
            {
                var databaseRecord = store
                    .Maintenance
                    .Server
                    .Send(new GetDatabaseRecordOperation(dbName));
                topology = databaseRecord?.Topology;
            } while (topology != null);
        }

        public async Task AddDocumentsToDbAsync(string path)
        {
            using (var stream = string.IsNullOrWhiteSpace(path) ? GetEmbeddedLastFmSubset() : System.IO.File.OpenRead(path))
            {
                var options = new DatabaseSmugglerImportOptions();

                await ((DocumentStore)DocumentStoreHolder.MediaStore).Smuggler.ImportAsync(options, stream)
                    .ConfigureAwait(false);
            }
        }

        private Stream GetEmbeddedLastFmSubset()
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream($"{typeof(Program).Namespace}.Dumps.small-media.ravendbdump");
        }
    }
}
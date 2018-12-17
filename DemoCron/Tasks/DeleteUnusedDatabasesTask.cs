using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoCommon.Utils;
using DemoCron.Helpers;
using Raven.Client.Documents;
using Raven.Client.ServerWide.Operations;

namespace DemoCron.Tasks
{
    internal class DeleteUnusedDatabasesTask : CliTask
    {
        private readonly DocumentStoreHolder _documentStoreHolder;
        private readonly TimeSpan _expirationSpan;

        public DeleteUnusedDatabasesTask(DocumentStoreHolder documentStoreHolder, Settings settings)
        {
            _documentStoreHolder = documentStoreHolder;

            var expirationSettings = settings.DeleteUnusedDatabases.ExpirationSpan;
            _expirationSpan = expirationSettings.ToTimeSpan();
        }

        private IDocumentStore DocumentStore => _documentStoreHolder.Store;

        public override async Task Run()
        {
            var userDatabaseNames = await GetUserDatabaseNames();

            foreach (var dbName in userDatabaseNames)
                await ProcessDatabase(dbName);
        }

        private async Task<IEnumerable<string>> GetUserDatabaseNames()
        {
            var dbNames = await GetDatabaseNames();
            return dbNames.Where(x => x.StartsWith(DatabaseName.UserDatabasePrefix));
        }

        private async Task<List<string>> GetDatabaseNames()
        {
            const int pageSize = 50;
            var results = new List<string>();

            string[] dbNames;
            var start = 0;

            do
            {
                var operation = new GetDatabaseNamesOperation(start, pageSize);
                dbNames = await DocumentStore.Maintenance.Server.SendAsync(operation);
                results.AddRange(dbNames);
                start += pageSize;
            } while (dbNames.Length > 0);

            return results;
        }

        private async Task ProcessDatabase(string databaseName)
        {
            if (await IsDatabaseUnused(databaseName))
                await DeleteDatabase(databaseName);
        }

        private async Task<bool> IsDatabaseUnused(string databaseName)
        {
            using (var session = DocumentStore.OpenAsyncSession(databaseName))
            {
                var stats = await session.LoadAsync<DemoStats>(DemoStats.DocumentId);
                return stats == null || AreStatsOldEnough(stats);
            }
        }

        private bool AreStatsOldEnough(DemoStats stats)
        {
            var expirationDateUtc = stats.LastAccessedUtc.Add(_expirationSpan);
            return DateTime.UtcNow > expirationDateUtc;
        }

        private Task DeleteDatabase(string databaseName)
        {
            var operation = new DeleteDatabasesOperation(databaseName, hardDelete: true);
            return DocumentStore.Maintenance.Server.SendAsync(operation);
        }
    }
}

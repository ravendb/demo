using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoCommon.Utils.Database;
using DemoCron.Helpers;
using Raven.Client.Documents;

namespace DemoCron.Tasks
{
    internal class DeleteUnusedDatabasesTask : CliTask
    {
        private readonly DatabaseApi _databaseApi = new DatabaseApi();

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
            var userDbPrefix = $"{DatabaseName.UserDatabasePrefix}-";
            var mediaDbPrefix = $"{DatabaseName.MediaDatabasePrefix}-";

            var dbNames = await _databaseApi.GetDatabaseNames(DocumentStore);
            return dbNames.Where(x => x.StartsWith(userDbPrefix) || x.StartsWith(mediaDbPrefix));
        }

        private async Task ProcessDatabase(string databaseName)
        {
            if (await IsDatabaseUnused(databaseName))
                await _databaseApi.DeleteDatabase(DocumentStore, databaseName);
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
    }
}

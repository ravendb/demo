using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoCommon.Utils.Database;
using DemoCron.Helpers;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;

namespace DemoCron.Tasks
{
    internal class DeleteUnusedDatabasesTask : CliTask
    {
        private readonly DatabaseApi _databaseApi = new DatabaseApi();

        private readonly DocumentStoreHolder _documentStoreHolder;
        private readonly TimeSpan _expirationSpan;
        private readonly ILogger _logger;

        public DeleteUnusedDatabasesTask(DocumentStoreHolder documentStoreHolder, Settings settings, ILogger<DeleteUnusedDatabasesTask> logger)
        {
            _documentStoreHolder = documentStoreHolder;
            _logger = logger;

            var expirationSettings = settings.DeleteUnusedDatabases.ExpirationSpan;
            _expirationSpan = expirationSettings.ToTimeSpan();
        }

        private IDocumentStore DocumentStore => _documentStoreHolder.Store;

        public override async Task Run()
        {
            _logger.LogInformation($"{nameof(DeleteUnusedDatabasesTask)} started.");

            var userDatabaseNames = await GetUserDatabaseNames();

            foreach (var dbName in userDatabaseNames)
                await ProcessDatabase(dbName);

            _logger.LogInformation($"{nameof(DeleteUnusedDatabasesTask)} finished.");
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
            var isUnused = await IsDatabaseUnused(databaseName);

            if (isUnused == false)
                return;

            _logger.LogInformation($"Removing database {databaseName}");
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

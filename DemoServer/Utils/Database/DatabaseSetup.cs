using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoCommon.Utils.Database;
using DemoServer.Utils.Cache;
using Raven.Client.Documents;

namespace DemoServer.Utils.Database
{
    public class DatabaseSetup
    {
        private readonly DatabaseApi _databaseApi = new DatabaseApi();

        private readonly UserStoreCache _userStoreCache;
        private readonly MediaStoreCache _mediaStoreCache;

        public DatabaseSetup(UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache)
        {
            _userStoreCache = userStoreCache;
            _mediaStoreCache = mediaStoreCache;
        }

        public async Task EnsureUserDatabaseExists(Guid userId)
        {
            var documentStore = _userStoreCache.GetEntry(userId);

            if (_databaseApi.DoesDatabaseExist(documentStore) == false)
            {
                await _databaseApi.CreateDatabase(documentStore);
                await _databaseApi.CreateSampleData(documentStore);
            }

            await UpdateDemoStats(documentStore);
        }

        public async Task EnsureMediaDatabaseExists(Guid userId)
        {
            var documentStore = _mediaStoreCache.GetEntry(userId);

            if (_databaseApi.DoesDatabaseExist(documentStore) == false)
            {
                await _databaseApi.CreateDatabase(documentStore);

                using (var dumpStream = GetMediaDump())
                {
                    await _databaseApi.ImportDump(documentStore, dumpStream);
                }
            }

            await UpdateDemoStats(documentStore);
        }

        private Stream GetMediaDump()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream("DemoServer.Data.media_subset.ravendbdump");
        }

        private async Task UpdateDemoStats(IDocumentStore documentStore)
        {
            using (var session = documentStore.OpenAsyncSession())
            {
                var demoStats = await session.LoadAsync<DemoStats>(DemoStats.DocumentId);

                if (demoStats == null)
                {
                    demoStats = new DemoStats();
                    await session.StoreAsync(demoStats);

                }

                demoStats.UpdateLastAccess();

                await session.SaveChangesAsync();
            }
        }

        public Task DeleteUserDatabase(Guid userId)
        {
            var documentStore = _userStoreCache.GetEntry(userId);
            return _databaseApi.DeleteDatabase(documentStore);
        }

        public Task DeleteMediaDatabase(Guid userId)
        {
            var documentStore = _mediaStoreCache.GetEntry(userId);
            return _databaseApi.DeleteDatabase(documentStore);
        }

        public async Task ResetUserDatabase(Guid userId)
        {
            await DeleteUserDatabase(userId);
            await EnsureUserDatabaseExists(userId);
        }

        public async Task ResetMediaDatabase(Guid userId)
        {
            await DeleteMediaDatabase(userId);
            await EnsureMediaDatabaseExists(userId);
        }

        public async Task EnsureUserDocumentExists<T>(Guid userId, string documentId, T document)
        {
            var documentStore = _userStoreCache.GetEntry(userId);

            using (var session = documentStore.OpenAsyncSession())
            {
                var doc = await session.LoadAsync<T>(documentId);
                if (doc == null)
                {
                    await session.StoreAsync(document, documentId);
                    await session.SaveChangesAsync();
                }
            }
        }

        public async Task EnsureUserCollectionExists<TDocument>(Guid userId, IEnumerable<TDocument> defaultDocuments)
        {
            var documentStore = _userStoreCache.GetEntry(userId);

            using (var session = documentStore.OpenAsyncSession())
            {
                var anyDocumentExists = await session.Query<TDocument>().AnyAsync();

                if (anyDocumentExists)
                    return;
            }

            await _databaseApi.BulkInsertDocuments(documentStore, defaultDocuments);
        }
    }
}

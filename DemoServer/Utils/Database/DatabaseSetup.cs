using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCommon.Models;
using DemoCommon.Utils.Database;
using DemoServer.Utils.Cache;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DemoServer.Utils.Database
{
    public class DatabaseSetup
    {
        private readonly DatabaseApi _databaseApi = new DatabaseApi();

        private readonly DocumentStoreCache _documentStoreCache;

        public DatabaseSetup(DocumentStoreCache documentStoreCache)
        {
            _documentStoreCache = documentStoreCache;
        }

        private IDocumentStore GetDocumentStore(Guid userId) => _documentStoreCache.GetEntry(userId);

        private IAsyncDocumentSession OpenAsyncSession(Guid userId)
        {
            var databaseName = DatabaseName.For(userId);
            var documentStore = _documentStoreCache.GetEntry(userId);
            return documentStore.OpenAsyncSession(databaseName);
        }

        public async Task EnsureUserDatabaseExists(Guid userId)
        {
            var documentStore = _documentStoreCache.GetEntry(userId);

            if (_databaseApi.DoesDatabaseExist(documentStore) == false)
            {
                await _databaseApi.CreateDatabase(documentStore);
                await _databaseApi.CreateSampleData(documentStore);
            }

            await UpdateDemoStats(userId);
        }

        private async Task UpdateDemoStats(Guid userId)
        {
            using (var session = OpenAsyncSession(userId))
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

        public Task DeleteDatabase(Guid userId)
        {
            var documentStore = GetDocumentStore(userId);
            return _databaseApi.DeleteDatabase(documentStore);
        }

        public async Task ResetDatabase(Guid userId)
        {
            await DeleteDatabase(userId);
            await EnsureUserDatabaseExists(userId);
        }

        public async Task EnsureDocumentExists<T>(Guid userId, string documentId, T document)
        {
            using (var session = OpenAsyncSession(userId))
            {
                var doc = await session.LoadAsync<T>(documentId);
                if (doc == null)
                {
                    await session.StoreAsync(document, documentId);
                    await session.SaveChangesAsync();
                }
            }
        }

        public async Task EnsureCollectionExists<TDocument>(Guid userId, IEnumerable<TDocument> defaultDocuments)
        {
            using (var session = OpenAsyncSession(userId))
            {
                var anyDocumentExists = await session.Query<TDocument>().AnyAsync();

                if (anyDocumentExists)
                    return;
            }

            var documentStore = GetDocumentStore(userId);
            await _databaseApi.BulkInsertDocuments(documentStore, defaultDocuments);
        }
    }
}

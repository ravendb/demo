using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoServer.Models;
using DemoServer.Utils.Cache;
using Raven.Client.Documents;

namespace DemoServer.Utils.Database
{
    public class DatabaseSetup
    {
        private readonly DocumentStoreCache _documentStoreCache;
        private readonly DatabaseAccessor _databaseAccessor;

        public DatabaseSetup(DocumentStoreCache documentStoreCache, DatabaseAccessor databaseAccessor)
        {
            _documentStoreCache = documentStoreCache;
            _databaseAccessor = databaseAccessor;
        }

        private IDocumentStore GetDocumentStore(Guid userId) => _documentStoreCache.GetEntry(userId);

        public async Task EnsureUserDatabaseExists(Guid userId)
        {
            var documentStore = _documentStoreCache.GetEntry(userId);

            if (_databaseAccessor.DoesDatabaseExist(documentStore) == false)
                _databaseAccessor.CreateDatabase(documentStore);

            await UpdateDemoStats(userId);
        }

        private async Task UpdateDemoStats(Guid userId)
        {
            using (var session = _databaseAccessor.OpenAsyncSession(userId))
            {
                var demoStats = await session.LoadAsync<DemoStats>(DemoStats.DocumentId);

                if (demoStats == null)
                    await session.StoreAsync(new DemoStats());
                else
                    demoStats.UpdateLastAccess();

                await session.SaveChangesAsync();
            }
        }

        public async Task ResetDatabase(Guid userId)
        {
            await _databaseAccessor.DeleteDatabase(userId);
            await EnsureUserDatabaseExists(userId);
        }

        public async Task EnsureDocumentExists<T>(Guid userId, string documentId, T document)
        {
            using (var session = _databaseAccessor.OpenAsyncSession(userId))
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
            using (var session = _databaseAccessor.OpenAsyncSession(userId))
            {
                var anyDocumentExists = await session.Query<TDocument>().AnyAsync();

                if (anyDocumentExists)
                    return;
            }

            var documentStore = GetDocumentStore(userId);
            await _databaseAccessor.BulkInsertDocuments(userId, documentStore, defaultDocuments);
        }
    }
}

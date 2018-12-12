using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoServer.Utils.Cache;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DemoServer.Utils.Database
{
    public class DatabaseAccessor
    {
        private readonly DocumentStoreCache _documentStoreCache;

        public DatabaseAccessor(DocumentStoreCache documentStoreCache)
        {
            _documentStoreCache = documentStoreCache;
        }

        public IAsyncDocumentSession OpenAsyncSession(Guid userId)
        {
            var databaseName = DatabaseName.For(userId);
            var documentStore = _documentStoreCache.GetEntry(userId);
            return documentStore.OpenAsyncSession(databaseName);
        }

        private IDocumentStore GetDocumentStore(Guid userId) => _documentStoreCache.GetEntry(userId);

        public void EnsureUserDatabaseExists(Guid userId)
        {
            var documentStore = _documentStoreCache.GetEntry(userId);

            if (DoesDatabaseExist(documentStore))
                return;

            CreateDatabase(documentStore);
        }

        private bool DoesDatabaseExist(IDocumentStore documentStore)
        {
            try
            {
                documentStore.Maintenance.ForDatabase(documentStore.Database).Send(new GetStatisticsOperation());
                return true;
            }
            catch (DatabaseDoesNotExistException)
            {
                return false;
            }
        }

        private void CreateDatabase(IDocumentStore documentStore)
        {
            try
            {
                var databaseRecord = new DatabaseRecord(documentStore.Database);
                var operation = new CreateDatabaseOperation(databaseRecord);
                documentStore.Maintenance.Server.Send(operation);
            }
            catch (ConcurrencyException)
            {
                // The database was already created before calling CreateDatabaseOperation
            }
        }

        public async Task ResetDatabase(Guid userId)
        {
            await DeleteDatabase(userId);
            EnsureUserDatabaseExists(userId);
        }

        public Task DeleteDatabase(Guid userId)
        {
            var documentStore = _documentStoreCache.GetEntry(userId);
            var operation = new DeleteDatabasesOperation(documentStore.Database, hardDelete: true);
            return documentStore.Maintenance.Server.SendAsync(operation);
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

        public async Task BulkInsertDocuments<T>(Guid userId, IList<T>documentsToStore) {

            using (var bulkInsert = GetDocumentStore(userId).BulkInsert())
            {
                foreach (var doc in documentsToStore)
                {
                    await bulkInsert.StoreAsync(doc);
                }
            } 
        }
    }
}

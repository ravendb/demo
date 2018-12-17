using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCommon.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database.Operations;
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

        public bool DoesDatabaseExist(IDocumentStore documentStore)
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

        public async Task CreateDatabase(IDocumentStore documentStore)
        {
            try
            {
                var databaseRecord = new DatabaseRecord(documentStore.Database);
                var operation = new CreateDatabaseOperation(databaseRecord);
                await documentStore.Maintenance.Server.SendAsync(operation);
            }
            catch (ConcurrencyException)
            {
                // The database was already created before calling CreateDatabaseOperation
            }
        }

        public Task CreateSampleData(IDocumentStore documentStore)
        {
            var operation = new CreateSampleDataOperation();
            return documentStore.Maintenance.SendAsync(operation);
        }

        public Task DeleteDatabase(Guid userId)
        {
            var documentStore = _documentStoreCache.GetEntry(userId);
            var operation = new DeleteDatabasesOperation(documentStore.Database, hardDelete: true);
            return documentStore.Maintenance.Server.SendAsync(operation);
        }

        public async Task BulkInsertDocuments<T>(Guid userId, IDocumentStore documentStore, IEnumerable<T> documentsToStore)
        {
            using (var bulkInsert = documentStore.BulkInsert())
            {
                foreach (var doc in documentsToStore)
                {
                    await bulkInsert.StoreAsync(doc);
                }
            }
        }
    }
}

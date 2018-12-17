using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCommon.Utils.Database.Operations;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DemoCommon.Utils.Database
{
    public class DatabaseApi
    {
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

        public Task DeleteDatabase(IDocumentStore documentStore, string databaseName)
        {
            var operation = new DeleteDatabasesOperation(databaseName, hardDelete: true);
            return documentStore.Maintenance.Server.SendAsync(operation);
        }

        public Task DeleteDatabase(IDocumentStore documentStore) => DeleteDatabase(documentStore, documentStore.Database);

        public async Task BulkInsertDocuments<T>(IDocumentStore documentStore, IEnumerable<T> documentsToStore)
        {
            using (var bulkInsert = documentStore.BulkInsert())
            {
                foreach (var doc in documentsToStore)
                {
                    await bulkInsert.StoreAsync(doc);
                }
            }
        }

        public async Task<List<string>> GetDatabaseNames(IDocumentStore documentStore)
        {
            const int pageSize = 50;
            var results = new List<string>();

            string[] dbNames;
            var start = 0;

            do
            {
                var operation = new GetDatabaseNamesOperation(start, pageSize);
                dbNames = await documentStore.Maintenance.Server.SendAsync(operation);

                results.AddRange(dbNames);
                start += pageSize;
            }
            while (dbNames.Length > 0);

            return results;
        }
    }
}

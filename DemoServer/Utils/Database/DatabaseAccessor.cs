using System;
using System.Threading.Tasks;
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
        private const string UserDatabasePrefix = "User-";

        private readonly DocumentStoreHolder _documentStoreHolder;
        private readonly Settings.DatabaseSettings _databaseSettings;

        public DatabaseAccessor(DocumentStoreHolder documentStoreHolder, Settings settings)
        {
            _documentStoreHolder = documentStoreHolder;
            _databaseSettings = settings.Database;
        }

        public string GetDatabaseName(Guid userId) => $"{UserDatabasePrefix}{userId.ToString()}";

        public string GetFirstDatabaseUrl()
        {
            return _databaseSettings.Urls[0];
        }

        private IDocumentStore GetDocumentStore(Guid userId)
        {
            var databaseName = GetDatabaseName(userId);
            return _documentStoreHolder.InitializeFor(databaseName);
        }

        private IAsyncDocumentSession GetSession(Guid userId)
        {
            var documentStore = GetDocumentStore(userId);
            return documentStore.OpenAsyncSession();
        }

        public void EnsureUserDatabaseExists(Guid userId)
        {
            var documentStore = GetDocumentStore(userId);

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

        public async Task SaveInitialDocument<T>(Guid userId, T document)
        {
            using (var session = GetSession(userId))
            {
                await session.StoreAsync(document);
                await session.SaveChangesAsync();
            }
        }

        public async Task DeleteDatabase(Guid userId)
        {
            var documentStore = GetDocumentStore(userId);
            await DeleteDatabase(documentStore);
        }

        private Task DeleteDatabase(IDocumentStore documentStore)
        {
            var operation = new DeleteDatabasesOperation(documentStore.Database, hardDelete: true);
            return documentStore.Maintenance.Server.SendAsync(operation);
        }
    }
}

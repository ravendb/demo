using System;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
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

        public DatabaseAccessor(DocumentStoreHolder documentStoreHolder)
        {
            _documentStoreHolder = documentStoreHolder;
        }

        public void EnsureUserDatabaseExists(Guid userId)
        {
            var documentStore = GetDocumentStore(userId);

            if (DoesDatabaseExist(documentStore))
                return;

            CreateDatabase(documentStore);
        }

        private IDocumentStore GetDocumentStore(Guid userId)
        {
            var databaseName = GetDatabaseName(userId);
            return _documentStoreHolder.InitializeFor(databaseName);
        }

        private string GetDatabaseName(Guid userId) => $"{UserDatabasePrefix}{userId.ToString()}";

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
    }
}

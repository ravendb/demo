using System;
using Raven.Client;
using Raven.Client.Document;

namespace DemoMethods
{
    public static class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> DocStore = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store
        {
            get { return DocStore.Value; }
        }

        public static string DatabaseName { get; set; }

        public static string ConnectionStringName { get; set; }

        public static string ServerUrl
        {
            get { return Store.Url; }
        }

        public static void SetDbInfo(string connectionStringName, string dbName)
        {
            ConnectionStringName = connectionStringName;
            DatabaseName = dbName;
        }

        private static IDocumentStore CreateStore()
        {
            var docStoreInit = new DocumentStore
            {
                ConnectionStringName = ConnectionStringName,
                DefaultDatabase = DatabaseName
            }.Initialize();

            return docStoreInit;
        }

    }
}

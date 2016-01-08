using System;
using Raven.Client;
using Raven.Client.Document;

namespace DemoMethods
{
    public static class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> DocStore = new Lazy<IDocumentStore>(CreateNorthwindStore);
        private static readonly Lazy<IDocumentStore> MediaDocStore = new Lazy<IDocumentStore>(CreateMediaStore);

        public static IDocumentStore Store => DocStore.Value;

        public static IDocumentStore MediaStore => MediaDocStore.Value;

        public static string NorthwindDatabaseName { get; set; }
        public static string MediaDatabaseName { get; set; }

        public static string ConnectionStringName { get; set; }

        public static string ServerUrl => Store.Url;

        public static void SetDbInfo(string connectionStringName, string dbNameNorthwind, string dbNameMedia)
        {
            ConnectionStringName = connectionStringName;
            NorthwindDatabaseName = dbNameNorthwind;
            MediaDatabaseName = dbNameMedia;
        }

        private static IDocumentStore CreateNorthwindStore()
        {
            var docStoreInit = new DocumentStore
            {
                ConnectionStringName = ConnectionStringName,
                DefaultDatabase = NorthwindDatabaseName
            }.Initialize();

            return docStoreInit;
        }

        private static IDocumentStore CreateMediaStore()
        {
            var docStoreInit = new DocumentStore
            {
                ConnectionStringName = ConnectionStringName,
                DefaultDatabase = MediaDatabaseName
            }.Initialize();

            return docStoreInit;
        }
    }
}

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

        public static string Address { get; set; }
        public static int Port { get; set; }
        public static string DatabaseName { get; set; }

        public static void SetDbInfo(string dbUrl, int dbPort, string dbName)
        {
            Address = dbUrl;
            Port = dbPort;
            DatabaseName = dbName;
        }

        private static IDocumentStore CreateStore()
        {
            var docStoreInit = new DocumentStore()
            {
                Url = string.Format("http://{0}:{1}", Address, Port),
                DefaultDatabase = DatabaseName
            }.Initialize();

            return docStoreInit;
        }

    }
}

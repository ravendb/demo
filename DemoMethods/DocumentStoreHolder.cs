using System;
using Raven.Client;
using Raven.Client.Document;

namespace DemoMethods
{
    public class DocumentStoreHolder
    {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public IDocumentStore Store
        {
            get { return store.Value; }
        }

        public static String Address { get; set; }
        public static int Port { get; set; }
        public static String DatabaseName { get; set; }

        public static void SetDbInfo(string DbUrl, int DbPort, string DbName)
        {
            Address = DbUrl;
            Port = DbPort;
            DatabaseName = DbName;
        }

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                Url = String.Format("http://{0}:{1}", Address, Port),
                DefaultDatabase = DatabaseName
            }.Initialize();

            return store;
        }

    }
}

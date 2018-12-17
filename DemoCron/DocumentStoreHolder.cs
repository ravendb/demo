using System;
using DemoCommon.Models;
using Raven.Client.Documents;

namespace DemoCron
{
    internal class DocumentStoreHolder
    {
        private readonly DatabaseSettings _databaseSettings;

        private readonly Lazy<IDocumentStore> _lazyStore;

        public IDocumentStore Store => _lazyStore.Value;

        public DocumentStoreHolder(Settings settings)
        {
            _databaseSettings = settings.Database;
            _lazyStore = new Lazy<IDocumentStore>(CreateStore());
        }

        private IDocumentStore CreateStore()
        {
            var store = new DocumentStore
            {
                Urls = _databaseSettings.Urls,
                Database = _databaseSettings.Name
            }.Initialize();

            return store;
        }
    }
}

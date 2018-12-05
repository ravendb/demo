using System;
using DemoServer.Utils.Cache;
using Raven.Client.Documents;

namespace DemoServer.Utils.Database
{
    public class DocumentStoreHolderWrapper
    {
        private readonly DocumentStoreCache _documentStoreCache;
        private readonly Guid _userId;

        public DocumentStoreHolderWrapper(DocumentStoreCache documentStoreCache, Guid userId)
        {
            _documentStoreCache = documentStoreCache;
            _userId = userId;
        }

        public IDocumentStore Store => _documentStoreCache.GetEntry(_userId);
    }
}

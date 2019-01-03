using System;
using DemoCommon.Utils.Database;
using DemoServer.Utils.Database;
using Microsoft.Extensions.Caching.Memory;
using Raven.Client.Documents;

namespace DemoServer.Utils.Cache
{
    public class DocumentStoreCache
    {
        private const string KeyPrefix = "DocumentStore-";
        private static TimeSpan FiveMinutes => TimeSpan.FromMinutes(5);

        private readonly IMemoryCache _memoryCache;
        private readonly DocumentStoreHolder _documentStoreHolder;
        private readonly DatabaseName _databaseName;

        public DocumentStoreCache(IMemoryCache memoryCache, DocumentStoreHolder documentStoreHolder,
            DatabaseName databaseName)
        {
            _memoryCache = memoryCache;
            _documentStoreHolder = documentStoreHolder;
            _databaseName = databaseName;
        }

        private string GetKeyName(Guid userId) => $"{KeyPrefix}{userId}";

        public IDocumentStore GetEntry(Guid userId)
        {
            var keyName = GetKeyName(userId);
            var cacheEntry = _memoryCache.GetOrCreate(keyName, entry => SetEntry(entry, userId));
            return cacheEntry;
        }

        private IDocumentStore SetEntry(ICacheEntry cacheEntry, Guid userId)
        {
            cacheEntry.SlidingExpiration = FiveMinutes;
            var databaseName = _databaseName.For(userId);

            return _documentStoreHolder.CreateStore(databaseName);
        }
    }
}

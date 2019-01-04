using System;
using DemoCommon.Utils.Database;
using DemoServer.Utils.Database;
using Microsoft.Extensions.Caching.Memory;
using Raven.Client.Documents;

namespace DemoServer.Utils.Cache
{
    public abstract class DocumentStoreCache
    {
        private static TimeSpan FiveMinutes => TimeSpan.FromMinutes(5);

        private readonly IMemoryCache _memoryCache;
        private readonly DocumentStoreHolder _documentStoreHolder;

        protected DocumentStoreCache(IMemoryCache memoryCache, DocumentStoreHolder documentStoreHolder)
        {
            _memoryCache = memoryCache;
            _documentStoreHolder = documentStoreHolder;
        }

        protected abstract string KeyPrefix();
        protected abstract string GetDatabaseName(Guid userId);

        private string GetKeyName(Guid userId) => $"{KeyPrefix()}{userId}";

        public IDocumentStore GetEntry(Guid userId)
        {
            var keyName = GetKeyName(userId);
            var cacheEntry = _memoryCache.GetOrCreate(keyName, entry => SetEntry(entry, userId));
            return cacheEntry;
        }

        private IDocumentStore SetEntry(ICacheEntry cacheEntry, Guid userId)
        {
            cacheEntry.SlidingExpiration = FiveMinutes;
            var databaseName = GetDatabaseName(userId);

            return _documentStoreHolder.CreateStore(databaseName);
        }
    }

    public class UserStoreCache : DocumentStoreCache
    {
        private readonly DatabaseName _databaseName;

        public UserStoreCache(IMemoryCache memoryCache, DocumentStoreHolder documentStoreHolder, DatabaseName databaseName) :
            base(memoryCache, documentStoreHolder)
        {
            _databaseName = databaseName;
        }

        protected override string KeyPrefix() => "UserStore-";

        protected override string GetDatabaseName(Guid userId) => _databaseName.For(userId);
    }

    public class MediaStoreCache : DocumentStoreCache
    {
        private readonly DatabaseName _databaseName;

        public MediaStoreCache(IMemoryCache memoryCache, DocumentStoreHolder documentStoreHolder, DatabaseName databaseName)
            : base(memoryCache, documentStoreHolder)
        {
            _databaseName = databaseName;
        }

        protected override string KeyPrefix() => "MediaStore-";

        protected override string GetDatabaseName(Guid userId) => _databaseName.MediaFor(userId);
    }
}

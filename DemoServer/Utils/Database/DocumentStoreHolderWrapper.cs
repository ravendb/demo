using System;
using DemoServer.Utils.Cache;
using Raven.Client.Documents;

namespace DemoServer.Utils.Database
{
    public class DocumentStoreHolderWrapper
    {
        private readonly UserStoreCache _userStoreCache;
        private readonly MediaStoreCache _mediaStoreCache;
        private readonly Guid _userId;

        public DocumentStoreHolderWrapper(UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache, Guid userId)
        {
            _userStoreCache = userStoreCache;
            _mediaStoreCache = mediaStoreCache;
            _userId = userId;
        }

        public IDocumentStore Store => _userStoreCache.GetEntry(_userId);

        public IDocumentStore MediaStore => _mediaStoreCache.GetEntry(_userId);
    }
}

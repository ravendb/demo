using System;
using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Conventions;
using DemoServer.Utils.Database;
using DemoServer.Utils.Filters;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace DemoServer.Controllers.Demos
{
    [ExecuteDemoRoute]
    [AddUserIdToHeader]
    public abstract class DemoCodeController : Controller
    {
        private readonly HeadersAccessor _headersAccessor;
        private readonly DocumentStoreCache _documentStoreCache;

        protected readonly DatabaseAccessor DatabaseAccessor;

        protected DemoCodeController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache, DatabaseAccessor databaseAccessor)
        {
            _headersAccessor = headersAccessor;
            _documentStoreCache = documentStoreCache;

            DatabaseAccessor = databaseAccessor;
        }

        [HttpPost]
        public async Task SetPrerequisites()
        {
            DatabaseAccessor.EnsureUserDatabaseExists(UserId);
            await SetDemoPrerequisites();
        }

        protected virtual Task SetDemoPrerequisites() => Task.CompletedTask;

        protected Guid UserId => _headersAccessor.GetUserIdFromRequest();

        protected DocumentStoreHolderWrapper DocumentStoreHolder =>
            new DocumentStoreHolderWrapper(_documentStoreCache, UserId);

        protected IAsyncDocumentSession OpenAsyncSession() => DatabaseAccessor.OpenAsyncSession(UserId);
    }
}

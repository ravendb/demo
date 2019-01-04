using System;
using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Conventions;
using DemoServer.Utils.Database;
using DemoServer.Utils.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos
{
    [ExecuteDemoRoute]
    [AddUserIdToHeader]
    public abstract class DemoCodeController : Controller
    {
        private readonly HeadersAccessor _headersAccessor;
        private readonly UserStoreCache _userStoreCache;

        protected readonly DatabaseSetup DatabaseSetup;

        protected DemoCodeController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, DatabaseSetup databaseSetup)
        {
            _headersAccessor = headersAccessor;
            _userStoreCache = userStoreCache;

            DatabaseSetup = databaseSetup;
        }

        [HttpPost]
        public async Task SetPrerequisites()
        {
            await DatabaseSetup.EnsureUserDatabaseExists(UserId);
            await SetDemoPrerequisites();
        }

        protected virtual Task SetDemoPrerequisites() => Task.CompletedTask;

        protected Guid UserId => _headersAccessor.GetUserIdFromRequest();

        protected DocumentStoreHolderWrapper DocumentStoreHolder =>
            new DocumentStoreHolderWrapper(_userStoreCache, UserId);
    }
}

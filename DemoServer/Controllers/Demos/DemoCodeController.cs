using System;
using System.Threading.Tasks;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Conventions;
using DemoServer.Utils.Database;
using DemoServer.Utils.Filters;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos
{
    [ExecuteDemoRoute]
    [ServiceFilter(typeof(AddUserIdToResponseHeaderAttribute))]
    public abstract class DemoCodeController : Controller
    {
        private readonly UserIdContainer _userId;
        private readonly UserStoreCache _userStoreCache;
        private readonly MediaStoreCache _mediaStoreCache;

        protected readonly DatabaseSetup DatabaseSetup;

        protected DemoCodeController(UserIdContainer userId, UserStoreCache userStoreCache,
            MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup)
        {
            _userId= userId;
            _userStoreCache = userStoreCache;
            _mediaStoreCache = mediaStoreCache;

            DatabaseSetup = databaseSetup;
        }

        [HttpPost]
        public async Task SetPrerequisites()
        {
            await DatabaseSetup.EnsureUserDatabaseExists(UserId);
            await SetDemoPrerequisites();
        }

        protected virtual Task SetDemoPrerequisites() => Task.CompletedTask;

        protected Guid UserId => _userId.Get();

        protected DocumentStoreHolderWrapper DocumentStoreHolder =>
            new DocumentStoreHolderWrapper(_userStoreCache, _mediaStoreCache, UserId);
    }
}

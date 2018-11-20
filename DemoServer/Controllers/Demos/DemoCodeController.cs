using System;
using System.Threading.Tasks;
using DemoServer.Utils;
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
        protected readonly DatabaseAccessor DatabaseAccessor;

        protected DemoCodeController(HeadersAccessor headersAccessor, DatabaseAccessor databaseAccessor)
        {
            _headersAccessor = headersAccessor;
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
    }
}

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
        private readonly DatabaseAccessor _databaseAccessor;

        protected DemoCodeController(HeadersAccessor headersAccessor, DatabaseAccessor databaseAccessor)
        {
            _headersAccessor = headersAccessor;
            _databaseAccessor = databaseAccessor;
        }

        [HttpPost]
        public async Task SetPrerequisites()
        {
            _databaseAccessor.EnsureUserDatabaseExists(UserId);
            await SetDemoPrerequisites();
        }

        protected abstract Task SetDemoPrerequisites();

        protected Guid UserId => _headersAccessor.GetUserIdFromRequest();
    }
}

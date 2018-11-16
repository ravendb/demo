using System;
using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Conventions;
using DemoServer.Utils.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos
{
    [ExecuteDemoRoute]
    [AddUserIdToHeader]
    public abstract class DemoCodeController : Controller
    {
        private readonly HeadersAccessor _headersAccessor;

        protected DemoCodeController(HeadersAccessor headersAccessor)
        {
            _headersAccessor = headersAccessor;
        }

        [HttpPost]
        public abstract Task SetPrerequisites();

        protected Guid UserId => _headersAccessor.GetUserIdFromRequest();
    }
}

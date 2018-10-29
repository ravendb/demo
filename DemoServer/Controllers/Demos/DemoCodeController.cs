using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos
{
    public abstract class DemoCodeController : Controller
    {
        protected abstract void SetPrerequisites();
    }
}

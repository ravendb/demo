using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos
{
    public abstract class DemoController : Controller
    {
        protected abstract void SetPrerequisites();
    }
}

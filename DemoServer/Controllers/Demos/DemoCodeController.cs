using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos
{
    public abstract class DemoCodeController : Controller
    {
        [HttpPost]
        public abstract Task SetPrerequisites();
    }
}

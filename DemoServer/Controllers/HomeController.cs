using DemoServer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly DemoContainer _demoContainer;

        public HomeController(DemoContainer demoContainer)
        {
            _demoContainer = demoContainer;
        }
    }
}

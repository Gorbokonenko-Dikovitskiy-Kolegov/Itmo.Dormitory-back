using Microsoft.AspNetCore.Mvc;

namespace Itmo.Dormitory.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

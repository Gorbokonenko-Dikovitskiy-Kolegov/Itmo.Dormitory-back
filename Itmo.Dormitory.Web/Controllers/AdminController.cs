using Microsoft.AspNetCore.Mvc;

namespace Itmo.Dormitory.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

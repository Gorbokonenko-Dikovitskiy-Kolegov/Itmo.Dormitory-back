using Microsoft.AspNetCore.Mvc;

namespace Itmo.Dormitory.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginAsResident()
        {
            return View();
        }
        public IActionResult LoginAsAdmin()
        {
            return View();
        }
    }
}

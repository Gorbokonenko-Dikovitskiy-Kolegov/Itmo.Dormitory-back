using Itmo.Dormitory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace Itmo.Dormitory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Announcements()
        {
            return RedirectToAction("Index", "Announcement");
        }

        public IActionResult Applications()
        {
            return RedirectToAction("Index", "Application");
        }

        public IActionResult Reservations()
        {
            return RedirectToAction("Index", "Reservation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

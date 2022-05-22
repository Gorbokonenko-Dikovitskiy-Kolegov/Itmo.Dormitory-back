using Itmo.Dormitory.API.Infrastructure.Middlewares;
using Itmo.Dormitory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;

namespace Itmo.Dormitory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ActivityTracker _tracker;


        public HomeController(ILogger<HomeController> logger, ActivityTracker tracker)
        {
            _logger = logger;
            _tracker = tracker;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Announcements()
        {
            _tracker.EndpointVissited("Announcement");
            return RedirectToAction("Index", "Announcement");
        }

        public IActionResult Applications()
        {
            _tracker.EndpointVissited("Application");
            return RedirectToAction("Index", "Application");
        }

        public IActionResult Reservations()
        {
            _tracker.EndpointVissited("Reservation");
            return RedirectToAction("Index", "Reservation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

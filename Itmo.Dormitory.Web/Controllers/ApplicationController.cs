using Itmo.Dormitory.API.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.Dormitory.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly ActivityTracker _tracker;

        public ApplicationController(ActivityTracker tracker)
        {
            _tracker = tracker;
        }
        public IActionResult Index()
        {
            _tracker.EndpointVissited("Applications");
            return View();
        }
    }
}

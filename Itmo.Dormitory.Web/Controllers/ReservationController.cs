using Microsoft.AspNetCore.Mvc;


namespace Itmo.Dormitory.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

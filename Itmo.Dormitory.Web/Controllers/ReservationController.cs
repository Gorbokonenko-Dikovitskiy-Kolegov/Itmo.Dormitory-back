using Itmo.Dormitory.Core.Reservations;
using Microsoft.AspNetCore.Mvc;


namespace Itmo.Dormitory.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationsAPIController _controller;
        public ReservationController(ReservationsAPIController controller)
        {
            _controller = controller;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Reserve(string roomName)
        {
            ViewBag.Slots = _controller.GetAvailableSlots(roomName).Result.Results;
            return View();
        }
        
        public IActionResult Reserved(int id)
        {
            ViewBag.Success = _controller.ReserveSlot(id).Result;
            return View();
        }
    }
}

using Itmo.Dormitory.Core.Reservations;
using Microsoft.AspNetCore.Authorization;
using Itmo.Dormitory.Core.Reservations.Commands;
using Microsoft.AspNetCore.Mvc;


namespace Itmo.Dormitory.Controllers
{
    [Authorize]
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
            ViewBag.Success = _controller.ReserveSlot(new ReserveSlot.Command(id, "281704")).Result.ReserveSuccessful;
            return View();
        }
        
        public IActionResult MyReservations()
        {
            ViewBag.Reservations = _controller.GetReservationsByOwner("281704").Result.Results;
            return View();
        }
        
        public IActionResult CancelReservation(int id)
        {
            ViewBag.Success = _controller.CancelReservation(id).Result is OkResult;
            return View();
        }
    }
}
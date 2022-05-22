using Itmo.Dormitory.Core.Reservations;
using Microsoft.AspNetCore.Authorization;
using Itmo.Dormitory.Core.Reservations.Commands;
using Itmo.Dormitory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Itmo.Dormitory.API.Infrastructure.Middlewares;

namespace Itmo.Dormitory.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ReservationsAPIController _controller;

        private readonly ActivityTracker _tracker;

        public ReservationController(ReservationsAPIController controller, ActivityTracker tracker)
        {
            _controller = controller;
            _tracker = tracker;
        }
        public IActionResult Index()
        {
            _tracker.EndpointVissited("Reservations");
            return View();
        }
        
        public IActionResult Reserve(string roomName, int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.RoomName = roomName;
            ViewBag.Slots = _controller.GetAvailableSlots(roomName, page).Result;
            return View();
        }
        
        public IActionResult Reserved(int id)
        {
            ViewBag.Success = _controller.ReserveSlot(new ReserveSlot.Command(id, User.Identity.Name)).Result.ReserveSuccessful;
            return View();
        }
        
        public IActionResult MyReservations(int page = 1)
        {
            ViewBag.Page = page;
            ViewBag.Reservations = _controller.GetReservationsByOwner(User.Identity.Name, page).Result;
            return View();
        }
        
        public IActionResult CancelReservation(int id)
        {
            ViewBag.Success = _controller.CancelReservation(id).Result is OkResult;
            return View();
        }
        
        [HttpGet]
        public IActionResult AddSlot(string roomName)
        {
            TempData["RoomName"] = roomName;
            return View();
        }
        
        [HttpPost]
        public IActionResult AddSlot(DateTimePicker picker, string roomName)
        {
            TempData["Success"] = _controller.CreateSlot(new CreateSlot.Command(TempData["RoomName"].ToString(), picker.Starts)).Result.Success;
            return RedirectToAction("SlotAdded", "Reservation");
        }
        
        public IActionResult SlotAdded()
        {
            ViewBag.Success = TempData["Success"];
            return View();
        }
    }
}
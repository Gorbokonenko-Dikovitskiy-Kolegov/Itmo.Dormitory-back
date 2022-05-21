using Itmo.Dormitory.Core.Announcements;
using Itmo.Dormitory.Core.Announcements.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementsAPIController _announcementsAPIController;

        public AnnouncementController(AnnouncementsAPIController announcementsAPIController)
        {
            _announcementsAPIController = announcementsAPIController;
        }
        public IActionResult Index(int page = 1)
        {
            var announcements = _announcementsAPIController.GetAnnouncementsList(page).Result;
            ViewBag.Page = page;
            ViewBag.Announcements = announcements;
            
            return View();
        }

        [HttpPost("Announcement/CreateAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement(string title, string description)
        {
            await _announcementsAPIController.CreateAnnouncement(
                new CreateAnnouncement.Command(DateTime.Now, title, description));
            return RedirectToAction("Index", "Announcement");
        }

        [HttpPost("Announcement/DeleteAnnouncement")]
        public async Task<IActionResult> DeleteAnnouncement(string id)
        {
            await _announcementsAPIController.DeleteAnnouncementById(new DeleteAnnouncement.Command(Guid.Parse(id)));
            return RedirectToAction("Index", "Announcement");
        }

        [HttpPost("Announcement/EditAnnouncement")]
        public async Task<IActionResult> EditAnnouncement(string id, string title, string description)
        {
            await _announcementsAPIController.EditAnnouncement(
                new EditAnnouncement.Command(Guid.Parse(id), DateTime.Now, title, description));
            return RedirectToAction("Index", "Announcement");
        }

    }
}

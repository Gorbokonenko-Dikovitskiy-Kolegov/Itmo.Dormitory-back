using AutoMapper;
using Itmo.Dormitory.Core.Announcements;
using Itmo.Dormitory.Core.Announcements.Queries;
using Itmo.Dormitory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Itmo.Dormitory.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementsAPIController _announcementsAPIController;
        private readonly IMapper _mapper;

        public AnnouncementController(AnnouncementsAPIController announcementsAPIController)
        {
            _announcementsAPIController = announcementsAPIController;
            _mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<GetAnnouncementsList.Response.Announcement, Announcement>()).CreateMapper();
        }
        public IActionResult Index()
        {
            var announcements = GetAnnouncements();
            return View(announcements);
        }

        public List<Announcement> GetAnnouncements()
        {
            var announcementList = _announcementsAPIController.GetAnnouncementsList(
                new GetAnnouncementsList.Query()).Result.Value.Announcements;
            return _mapper.Map<IEnumerable<GetAnnouncementsList.Response.Announcement>, List<Announcement>>(announcementList);
        }
    }
}

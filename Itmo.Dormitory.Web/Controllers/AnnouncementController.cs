﻿using AutoMapper;
using Itmo.Dormitory.API.Infrastructure.Middlewares;
using Itmo.Dormitory.Core.Announcements;
using Itmo.Dormitory.Core.Announcements.Commands;
using Itmo.Dormitory.Core.Announcements.Queries;
using Itmo.Dormitory.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementsAPIController _announcementsAPIController;
        private readonly IMapper _mapper;
        private readonly ActivityTracker _tracker;

        public AnnouncementController(AnnouncementsAPIController announcementsAPIController, ActivityTracker tracker)
        {
            _announcementsAPIController = announcementsAPIController;
            _tracker = tracker;
            _mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<GetAnnouncementsList.Response.Announcement, Announcement>()).CreateMapper();
        }
        public IActionResult Index()
        {
            _tracker.EndpointVissited("Announcements");
            var announcements = GetAnnouncements();
            return View(announcements);
        }

        public List<Announcement> GetAnnouncements()
        {
            var announcementList = _announcementsAPIController.GetAnnouncementsList().Result.Value.Announcements;
            return _mapper.Map<IEnumerable<GetAnnouncementsList.Response.Announcement>, List<Announcement>>(announcementList);
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

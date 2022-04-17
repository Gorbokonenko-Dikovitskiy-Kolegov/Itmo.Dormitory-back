using Itmo.Dormitory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Itmo.Dormitory.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {
            var announcements = GetAnnouncements();
            return View(announcements);
        }

        public List<Announcement> GetAnnouncements()
        {
            var announcementList = new List<Announcement>();
            for(int i = 0; i < 8; i++)
            {
                var announcement = new Announcement 
                       { ID=Guid.NewGuid(), Title = $"{i} Объявление", Content = $"Содержимое {i} объявления", CreationTime = DateTime.Now.AddDays(-i) };
                announcementList.Add(announcement);
            }
            return announcementList;
        }
    }
}

using System;

namespace Itmo.Dormitory.Web.Models
{
    public class Announcement
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

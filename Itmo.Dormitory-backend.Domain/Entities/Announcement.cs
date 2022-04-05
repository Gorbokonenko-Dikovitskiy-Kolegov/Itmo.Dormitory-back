using Itmo.Dormitory_backend.Domain.ValueObjects;
using System;

namespace Itmo.Dormitory_backend.Domain.Entities
{
    public class Announcement
    {
        public Guid Id { get; private set; }
        public DateTime CreateTime { get; private init; }
        public DateTime LastUpdateTime { get; set; }
        public AttachedInformation Information { get; set; } = null!;
        private Announcement() { }
        public Announcement(Guid id, DateTime createTime, DateTime lastUpdateTime, AttachedInformation information)
        {
            Id = id;
            CreateTime = createTime;
            LastUpdateTime = lastUpdateTime;
            Information = information;
        }
    }
}

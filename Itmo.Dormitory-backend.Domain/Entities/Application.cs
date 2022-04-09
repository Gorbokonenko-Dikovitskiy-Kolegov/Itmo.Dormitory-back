using Itmo.Dormitory_backend.Domain.ValueObjects;
using System;

namespace Itmo.Dormitory_backend.Domain.Entities
{
    public class Application
    {
        public Guid Id { get; private set; }
        public DateTime LastUpdateTime { get; set; }
        public ApplicationType ApplicationType { get; set; }
        public AttachedInformation Information { get; set; } = null!;
        public Resident Resident { get; set; } = null!;
        public bool IsResolved { get; set; }
        private Application() { }
        public Application(Guid id, DateTime lastUpdateTime, ApplicationType applicationType, AttachedInformation information, Resident resident)
        {
            Id = id;
            LastUpdateTime = lastUpdateTime;
            ApplicationType = applicationType;
            Information = information;
            Resident = resident;
            IsResolved = false;
        }
    }
}

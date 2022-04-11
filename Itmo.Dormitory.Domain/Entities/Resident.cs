using Itmo.Dormitory.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Itmo.Dormitory.Domain.Entities
{
    public class Resident
    {
        private readonly List<Application> _applications = new List<Application>();
        public Guid Id { get; private set; }
        public PersonName Name { get; set; } = null!;
        public ISUNumber ISUNumber{ get; set; } = null!;
        public RoomNumber RoomNumber { get; set; } = null!;
        public IReadOnlyCollection<Application> Applications => _applications.AsReadOnly();
        private Resident() { }
        public Resident(Guid id, PersonName name, ISUNumber isuNumber, RoomNumber roomNumber)
        {
            Id = id;
            Name = name;
            ISUNumber = isuNumber;
            RoomNumber = roomNumber;
        }
        public void AddApplication(Application application)
        {
            _applications.Add(application);
        }
    }
}

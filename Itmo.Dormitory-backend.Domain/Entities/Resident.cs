using Itmo.Dormitory_backend.Domain.ValueObjects;
using System;


namespace Itmo.Dormitory_backend.Domain.Entities
{
    public class Resident
    {
        public Guid Id { get; private set; }
        public PersonName Name { get; set; } = null!;
        public ISUNumber ISUNumber{ get; set; } = null!;
        public RoomNumber RoomNumber { get; set; } = null!; 
        private Resident() { }
        public Resident(Guid id, PersonName name, ISUNumber isuNumber, RoomNumber roomNumber)
        {
            Id = id;
            Name = name;
            ISUNumber = isuNumber;
            RoomNumber = roomNumber;
        }
    }
}

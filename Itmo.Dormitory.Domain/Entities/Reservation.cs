using System;

namespace Itmo.Dormitory.Domain.Entities;

public class Reservation
{
    public int Id { get; init; }
    public string RoomName { get; init; }
    public DateTime Starts { get; init; }
    public bool Reserved { get; set; }
}
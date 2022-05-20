#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itmo.Dormitory.Domain.Entities;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string RoomName { get; init; }
    public DateTime Starts { get; init; }
    public bool Reserved { get; set; }
    public string? Owner { get; set; }
}
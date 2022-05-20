using Itmo.Dormitory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itmo.Dormitory.DataAccess.EntityConfiguration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
        builder.Property(r => r.RoomName);
        builder.Property(r => r.Starts);
        builder.Property(r => r.Reserved);
        builder.Property(r => r.Owner);
    }
}
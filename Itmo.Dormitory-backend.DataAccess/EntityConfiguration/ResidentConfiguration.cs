using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Itmo.Dormitory_backend.Domain.Entities;

namespace Itmo.Dormitory_backend.DataAccess.EntityConfigurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();

            builder.OwnsOne(t => t.Name);
            builder.OwnsOne(t => t.ISUNumber);
            builder.OwnsOne(t => t.RoomNumber);
            builder.Navigation(c => c.Applications).HasField("_applications");
            builder.HasMany(c => c.Applications).WithOne(r => r.Resident);
        }
    }
}
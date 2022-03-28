using Itmo.Dormitory_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Itmo.Dormitory_backend.DataAccess.EntityConfiguration
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();

            var informationConfigurationBuilder = builder.OwnsOne(t => t.Information);
            informationConfigurationBuilder.Property(pn => pn.Title).HasMaxLength(60);
            informationConfigurationBuilder.Property(pn => pn.Description).HasMaxLength(2000);
        }
    }
}

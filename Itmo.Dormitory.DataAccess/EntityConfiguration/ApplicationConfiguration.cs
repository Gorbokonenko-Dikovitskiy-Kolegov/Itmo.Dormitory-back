using Itmo.Dormitory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itmo.Dormitory.DataAccess.EntityConfiguration
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();

            var informationConfigurationBuilder = builder.OwnsOne(t => t.Information);
            informationConfigurationBuilder.Property(pn => pn.Title).HasMaxLength(60);
            informationConfigurationBuilder.Property(pn => pn.Description).HasMaxLength(2000);
        }
    }
}

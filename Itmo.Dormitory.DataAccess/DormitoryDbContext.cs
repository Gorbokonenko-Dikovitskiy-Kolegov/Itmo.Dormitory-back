using Itmo.Dormitory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Itmo.Dormitory.DataAccess
{
    public class DormitoryDbContext : DbContext
    {
        public DormitoryDbContext(DbContextOptions<DormitoryDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Resident> Residents { get; set; } = null!;
        public DbSet<Announcement> Announcements { get; set; } = null!;
        public DbSet<Application> Applications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

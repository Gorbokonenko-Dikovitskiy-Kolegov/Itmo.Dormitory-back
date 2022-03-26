using Itmo.Dormitory_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Itmo.Dormitory_backend.DataAccess
{
    public class DormitoryDbContext : DbContext
    {
        public DormitoryDbContext(DbContextOptions<DormitoryDbContext> options) : base(options) { }

        public DbSet<Resident> Residents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}

using Itmo.Dormitory.API.Infrastructure.Middlewares;
using Itmo.Dormitory.Core;
using Itmo.Dormitory.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Dormitory.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DormitoryDbContext>(
                o => o.UseSqlite($"Data Source={@"D:\STUDY\test.db"}")); // I will definitely remove it.Some day.
            services.AddControllers(options => options.Filters.Add(new GlobalExceptionFilter()));
            services.AddCoreModule();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DormitoryDbContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

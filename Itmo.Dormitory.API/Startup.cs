using Itmo.Dormitory.API.Infrastructure.Middlewares;
using Itmo.Dormitory.Core;
using Itmo.Dormitory.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            /*
            services.AddDbContext<DormitoryDbContext>(o =>
                 o.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            */
            services.AddDbContext<DormitoryDbContext>(o =>
                o.UseNpgsql(connectionString));
            services.AddControllers(options => options.Filters.Add(new GlobalExceptionFilter()));
            services.AddCoreModule();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DormitoryDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddSignInManager<SignInManager<IdentityUser>>();

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {          
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

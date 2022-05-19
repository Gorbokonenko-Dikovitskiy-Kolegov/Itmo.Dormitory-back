using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Core;
using Itmo.Dormitory.Core.Announcements;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Itmo.Dormitory.Web
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
            services.AddControllersWithViews();
            services.AddScoped<AnnouncementsAPIController>();
            services.AddCoreModule();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DormitoryDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddSignInManager<SignInManager<IdentityUser>>();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Index";
                options.AccessDeniedPath = "/Account/Index";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}");
            });
        }
    }
}

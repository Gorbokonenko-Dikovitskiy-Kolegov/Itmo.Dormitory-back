using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Core;
using Itmo.Dormitory.Core.Announcements;
using Itmo.Dormitory.Core.Reservations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Itmo.Dormitory.Core.SignalR;

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
            services.AddDbContext<DormitoryDbContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            /*services.AddDbContext<DormitoryDbContext>(
                o => o.UseSqlite(Configuration["ConnectionString"]));
            */
            services.AddControllersWithViews();
            services.AddScoped<AnnouncementsAPIController>();
            services.AddScoped<ReservationsAPIController>();
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

            services.AddSignalR();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<DormitoryHub>("/dormitory");
            });
        }
    }
}

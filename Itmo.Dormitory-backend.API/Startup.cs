using Itmo.Dormitory_backend.API.Infrastructure.Middlewares;
using Itmo.Dormitory_backend.Core;
using Itmo.Dormitory_backend.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Dormitory_backend.API
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
                o => o.UseInMemoryDatabase("DormitoryDb"));
            services.AddControllers(options => options.Filters.Add(new GlobalExceptionFilter()));
            services.AddCoreModule();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

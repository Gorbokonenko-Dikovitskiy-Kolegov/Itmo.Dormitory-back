using Itmo.Dormitory_backend.Core;
using Itmo.Dormitory_backend.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

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
                o => o.UseInMemoryDatabase("SdkDb"));

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Itmo.Dormitory_backend.API", Version = "v1" });

                options.CustomSchemaIds(selector =>
                {
                    var type = selector;
                    var typeNames = new List<string> { type.Name };

                    while (type?.IsNested ?? false)
                    {
                        type = type.DeclaringType;
                        typeNames.Add(type?.Name);
                    }

                    typeNames.Reverse();
                    return string.Join(".", typeNames);
                });
            });
            services.AddCoreModule();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Itmo.Dormitory_backend.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

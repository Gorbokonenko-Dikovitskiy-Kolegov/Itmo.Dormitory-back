using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Dormitory.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CoreModule).Assembly);

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(typeof(CoreModule)))
                .PartManager.ApplicationParts.Add(new AssemblyPart(typeof(CoreModule).Assembly));


            return services;
        }
    }
}
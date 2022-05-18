using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Itmo.Dormitory.API.Infrastructure.StartupFilters;
using System;

namespace Itmo.Dormitory.API.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Dormitory API",
                        Description = "Here you can manage the Dormitory",
                        Contact = new OpenApiContact
                        {
                            Name = "О возникших проблемах можно сообщить здесь",
                            Url = new Uri("https://github.com/Gorbokonenko-Dikovitskiy-Kolegov/Itmo.Dormitory-back/issues")
                        }
                    });

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
            });

            return builder;
        }
    }
}
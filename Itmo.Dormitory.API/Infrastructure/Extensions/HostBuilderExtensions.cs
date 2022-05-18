using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Itmo.Dormitory.API.Infrastructure.StartupFilters;
using System;
using System.Reflection;
using System.IO;
using System.Linq;

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

                    var currentAssembly = Assembly.GetExecutingAssembly();
                    var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new AssemblyName[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                    .Where(f => File.Exists(f)).ToArray();

                    Array.ForEach(xmlDocs, (d) =>
                    {
                        options.IncludeXmlComments(d);
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
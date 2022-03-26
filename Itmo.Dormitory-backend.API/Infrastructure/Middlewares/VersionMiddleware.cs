using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Itmo.Dormitory_backend.API.Infrastructure.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var versionObject = new
            {
                Version = assemblyName.Version?.ToString() ?? "version not specified",
                ServiceName = assemblyName.Name

            };

            await context.Response.WriteAsJsonAsync(versionObject);
        }
    }
}
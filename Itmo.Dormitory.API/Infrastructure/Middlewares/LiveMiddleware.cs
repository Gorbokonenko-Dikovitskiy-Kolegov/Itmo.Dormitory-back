using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Itmo.Dormitory.API.Infrastructure.Middlewares
{
    public class LiveMiddleware
    {
        public LiveMiddleware(RequestDelegate next)
        {
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;

            return Task.CompletedTask;
        }
    }
}
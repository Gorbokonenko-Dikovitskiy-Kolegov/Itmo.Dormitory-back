using Itmo.Dormitory.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Itmo.Dormitory.API.Infrastructure.Middlewares
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {                
            var result = new
            {
                ExceptionType = context.Exception.GetType().FullName,
                Message = context.Exception.Message,
            };

            int code = StatusCodes.Status500InternalServerError;
            if(context.Exception is IBaseException exception)
            {
                code = exception.StatusCode;
            }

            var jsonResult = new JsonResult(result)
            {
                StatusCode = code
            };

            context.ExceptionHandled = true;
            context.Result = jsonResult;
        }
    }
}
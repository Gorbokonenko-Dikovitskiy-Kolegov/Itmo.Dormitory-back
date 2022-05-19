using Microsoft.AspNetCore.Http;
using System;

namespace Itmo.Dormitory.Common.Exceptions
{
    public class EntityNotFoundException : Exception , IBaseException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public int StatusCode { get; } = StatusCodes.Status422UnprocessableEntity;
    }
}

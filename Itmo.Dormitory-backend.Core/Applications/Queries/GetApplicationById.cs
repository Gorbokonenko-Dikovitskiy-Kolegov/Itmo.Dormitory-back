using Itmo.Dormitory_backend.Common.Exceptions;
using Itmo.Dormitory_backend.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory_backend.Core.Applications.Queries
{
    public static class GetApplicationById
    {
        [PublicAPI]
        public record Query(Guid Id) : IRequest<Response>;

        [PublicAPI]
        public record Response(
            Guid Id,
            DateTime LastUpdateTime,
            Guid ResidentId,
            bool IsResolved,
            string? Title,
            string Description);

        [UsedImplicitly]
        public class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly DormitoryDbContext _dormitoryDbContext;

            public QueryHandler(DormitoryDbContext dormitoryDbContext)
            {
                _dormitoryDbContext = dormitoryDbContext;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {

                var application = await _dormitoryDbContext.Applications
                                     .Include(a => a.Resident)
                                     .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                if (application is null)
                    throw new EntityNotFoundException($"Application with Id {request.Id} not found");

                return new Response(
                    application.Id,
                    application.LastUpdateTime,
                    application.Resident.Id,
                    application.IsResolved,
                    application.Information.Title,
                    application.Information.Description);
            }
        }
    }
}

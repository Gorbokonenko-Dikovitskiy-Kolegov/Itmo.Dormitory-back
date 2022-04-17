using Itmo.Dormitory.Common.Exceptions;
using Itmo.Dormitory.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Applications.Queries
{
    public static class GetApplicationsByResident
    {
        [PublicAPI]
        public record Query(Guid ResidentId) : IRequest<Response>;

        [PublicAPI]
        public record Response(IReadOnlyCollection<Response.Application> Applications)
        {
            public record Application(
                Guid Id,
                DateTime LastUpdateTime,
                Guid ResidentId,
                bool IsResolved,
                string? Title,
                string Description);
        }

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
                var resident = await _dormitoryDbContext.Residents
                                        .Include(c => c.Applications)
                                        .FirstOrDefaultAsync(c => c.Id == request.ResidentId, cancellationToken);

                if (resident is null)
                    throw new EntityNotFoundException($"Resident with id {request.ResidentId}");

                var applications = resident.Applications
                                        .Select(a => new Response.Application(a.Id,
                                                                              a.LastUpdateTime,
                                                                              a.Resident.Id,
                                                                              a.IsResolved,
                                                                              a.Information.Title,
                                                                              a.Information.Description))
                                        .ToList();

                return new Response(applications);
            }
        }
    }
}

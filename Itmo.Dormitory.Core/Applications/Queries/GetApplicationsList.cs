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
    public static class GetApplicationsList
    {
        [PublicAPI]
        public record Query() : IRequest<Response>;

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

               
                var applications = await _dormitoryDbContext.Applications.Include(a => a.Resident).Select(
                    t => new Response.Application(
                        t.Id,
                        t.LastUpdateTime,
                        t.Resident.Id,
                        t.IsResolved,
                        t.Information.Title,
                        t.Information.Description)).ToListAsync(cancellationToken);
                return new Response(applications);
            }
        }
    }
}

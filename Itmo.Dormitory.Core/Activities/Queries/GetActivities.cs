using Itmo.Dormitory.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Activities.Queries
{
    public static class GetActivities
    {
        [PublicAPI]
        public record Query() : IRequest<Response>;

        [PublicAPI]
        public record Response(IReadOnlyCollection<Response.Counter> Counters)
        {
            public record Counter(
                int CurrentCount,
                string Route);
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
                var counters = await _dormitoryDbContext.Counters.Select(
                    t => new Response.Counter(
                        t.CurrentCount,
                        t.Route)).
                    ToListAsync(cancellationToken);

                return new Response(counters);
            }
        }
    }
}

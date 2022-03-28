using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Itmo.Dormitory_backend.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Itmo.Dormitory_backend.Core.Announcements.Queries
{
    public static class GetAnnouncementsList
    {
        [PublicAPI]
        public record Query() : IRequest<Response>;

        [PublicAPI]
        public record Response(IReadOnlyCollection<Response.Announcement> Announcements)
        {
            public record Announcement(
                Guid Id,
                DateTime LastUpdateTime,
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
                var announcements = await _dormitoryDbContext.Announcements.Select(
                    t => new Response.Announcement(
                        t.Id,
                        t.LastUpdateTime,
                        t.Information.Title,
                        t.Information.Description)).ToListAsync(cancellationToken);

                return new Response(announcements);
            }
        }
    }
}

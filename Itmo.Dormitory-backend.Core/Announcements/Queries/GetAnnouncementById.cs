using Itmo.Dormitory_backend.Common.Exceptions;
using Itmo.Dormitory_backend.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Itmo.Dormitory_backend.Core.Announcements.Queries
{
    public static class GetAnnouncementById
    {
        [PublicAPI]
        public record Query(Guid Id) : IRequest<Response>;

        [PublicAPI]
        public record Response(
            Guid Id,
            DateTime CreateTime,
            DateTime LastUpdateTime,
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
                //var resident = await _context.Residents.FirstAsync(t => t.Id == request.Id, cancellationToken);

                var announcement = await _dormitoryDbContext.Announcements.SingleOrDefaultAsync(
                    t => t.Id == request.Id, cancellationToken);

                if (announcement is null)
                    throw new EntityNotFoundException($"Announcement with Id {request.Id} not found");

                return new Response(
                    announcement.Id,
                    announcement.CreateTime,
                    announcement.LastUpdateTime,
                    announcement.Information.Title,
                    announcement.Information.Description);
            }
        }
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Itmo.Dormitory.Core.Announcements.Queries
{
    public static class GetAnnouncementsList
    {
        [PublicAPI]
        public record Query(int page) : IRequest<PagedResponse<Announcement>>;

        [UsedImplicitly]
        public class QueryHandler : IRequestHandler<Query, PagedResponse<Announcement>>
        {
            private readonly DormitoryDbContext _dormitoryDbContext;

            public QueryHandler(DormitoryDbContext dormitoryDbContext)
            {
                _dormitoryDbContext = dormitoryDbContext;
            }

            public async Task<PagedResponse<Announcement>> Handle(Query request, CancellationToken cancellationToken)
            {
                var announcements = _dormitoryDbContext.Announcements;
                
                var pageResults = 4f;
                var pageCount = Math.Ceiling(await announcements.CountAsync(cancellationToken) / pageResults);
                var items = await announcements
                    .OrderBy(a => a.CreateTime)
                    .Skip((request.page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToListAsync(cancellationToken);

                return new PagedResponse<Announcement>()
                {
                    Data = items, 
                    CurrentPage = request.page, 
                    Pages = (int) pageCount
                };
            }
        }
    }
}

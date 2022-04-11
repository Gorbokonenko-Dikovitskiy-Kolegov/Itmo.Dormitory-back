using Itmo.Dormitory.Common.Exceptions;
using Itmo.Dormitory.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Residents.Queries
{
    public static class GetResidentById
    {
        [PublicAPI]
        public record Query(Guid Id) : IRequest<Response>;

        [PublicAPI]
        public record Response(
            Guid Id,
            string FirstName,
            string LastName,
            string? MiddleName,
            string ISUNumber,
            string RoomNumber);

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

                var resident = await _dormitoryDbContext.Residents.SingleOrDefaultAsync(
                    t => t.Id == request.Id, cancellationToken);

                if (resident is null)
                    throw new EntityNotFoundException($"Resident with Id {request.Id} not found");

                return new Response(
                    resident.Id,
                    resident.Name.FirstName,
                    resident.Name.LastName,
                    resident.Name.MiddleName,
                    resident.ISUNumber.Value,
                    resident.RoomNumber.Value);
            }
        }
    }
}

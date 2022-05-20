using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using Itmo.Dormitory.Web.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Queries
{
    public static class GetAvailableSlots
    {
        public record Query(string RoomName) : IListRequest<Result>;
        
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.RoomName).NotEmpty();
                RuleFor(x => x.RoomName).InRange(RoomName.All);
            }
        }

        public record Result : IListResponse<Reservation>
        {
            public int Size { get; init; }

            public IList<Reservation> Results { get; init; } = new List<Reservation>();
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly DormitoryDbContext _db;

            public Handler(DormitoryDbContext db) => _db = db;

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                var reservations = await _db.Reservations
                    .Where(r => r.RoomName == query.RoomName)
                    .Where(r => r.Reserved == false)
                    .ToListAsync(cancellationToken);

                return new Result
                {
                    Size = reservations.Count,
                    Results = reservations
                };
            }
        }
    }
}
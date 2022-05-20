using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Queries;

public static class GetReservationsByOwner
{
    public record Query(string ISUNumber) : IListRequest<Result>;
        
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.ISUNumber).NotEmpty().Matches(@"^\d+$");
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
                .Where(r => r.Owner == query.ISUNumber)
                .ToListAsync(cancellationToken);

            return new Result
            {
                Size = reservations.Count,
                Results = reservations
            };
        }
    }
}
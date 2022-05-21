using System;
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
    public record Query(string ISUNumber, int page) : IRequest<PagedResponse<Reservation>>;
        
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.ISUNumber).NotEmpty().Matches(@"^\d+$");
        }
    }
    
    public class Handler : IRequestHandler<Query, PagedResponse<Reservation>>
    {
        private readonly DormitoryDbContext _db;

        public Handler(DormitoryDbContext db) => _db = db;

        public async Task<PagedResponse<Reservation>> Handle(Query query, CancellationToken cancellationToken)
        {
            var reservations = _db.Reservations
                .Where(r => r.Owner == query.ISUNumber);

            var pageResults = 3f;
            var pageCount = Math.Ceiling(await reservations.CountAsync(cancellationToken) / pageResults);
            var items = await reservations
                .OrderBy(r => r.Starts)
                .Skip((query.page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync(cancellationToken);

            return new PagedResponse<Reservation>()
            {
                Data = items, 
                CurrentPage = query.page, 
                Pages = (int) pageCount
            };
        }
    }
}
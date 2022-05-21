using System;
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
        public record Query(string RoomName, int page) : IRequest<PagedResponse<Reservation>>;
        
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.RoomName).NotEmpty();
                RuleFor(x => x.RoomName).InRange(RoomName.All);
            }
        }

        public class Handler : IRequestHandler<Query, PagedResponse<Reservation>>
        {
            private readonly DormitoryDbContext _db;

            public Handler(DormitoryDbContext db) => _db = db;

            public async Task<PagedResponse<Reservation>> Handle(Query query, CancellationToken cancellationToken)
            {
                var reservations = _db.Reservations
                    .Where(r => r.RoomName == query.RoomName)
                    .Where(r => r.Reserved == false);
                
                var pageResults = 4f;
                var pageCount = Math.Ceiling(await reservations.CountAsync(cancellationToken) / pageResults);
                var a = await reservations
                    .OrderBy(r => r.Starts)
                    .Skip((query.page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToListAsync(cancellationToken);

                return new PagedResponse<Reservation>()
                {
                    Data = a, 
                    CurrentPage = query.page, 
                    Pages = (int) pageCount
                };
            }
        }
    }
}
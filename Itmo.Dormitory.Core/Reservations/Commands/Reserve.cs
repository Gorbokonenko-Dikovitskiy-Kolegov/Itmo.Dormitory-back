using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Itmo.Dormitory.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Commands;

[ApiExplorerSettings(GroupName = "Reservations")]
public class Reserve : ControllerBase
{
    private readonly IMediator _mediator;
    
    public Reserve(IMediator mediator) => _mediator = mediator;
    
    /// <summary>
    /// Зарезервировать слот по id
    /// </summary>
    /// <param name="id" example="1"></param>
    [HttpPut("api/v1/reservations/reserve/{id}")]
    public async Task<bool> Action([FromHeader] int id)
    {
        return await _mediator.Send(new Query(id));
    }
    
    public record Query(int Id) : IListRequest<bool>;
    
    public class Handler : IRequestHandler<Query, bool>
    {
        private readonly DormitoryDbContext _db;

        public Handler(DormitoryDbContext db) => _db = db;

        public async Task<bool> Handle(Query query, CancellationToken cancellationToken)
        {
            var reservation = await _db.Reservations
                .Where(r => r.Id == query.Id)
                .Where(r => r.Reserved == false)
                .SingleOrDefaultAsync(cancellationToken);

            if (reservation is null)
            {
                return false;
            }
            
            reservation.Reserved = true;
            _db.Reservations.Update(reservation);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
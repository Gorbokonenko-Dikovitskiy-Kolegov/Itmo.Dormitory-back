using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Itmo.Dormitory.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Commands;

public class CancelReservation
{
    public record Command(int Id) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command>
    {
        private readonly DormitoryDbContext _db;

        public Handler(DormitoryDbContext db) => _db = db;

        public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            var reservation = await _db.Reservations
                .Where(r => r.Id == command.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (reservation != null)
            {
                reservation.Reserved = false;
                reservation.Owner = String.Empty;
                _db.Reservations.Update(reservation);
                await _db.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
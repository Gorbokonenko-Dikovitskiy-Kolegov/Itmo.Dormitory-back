using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Commands
{
    [ApiExplorerSettings(GroupName = "Reservations")]
    public class Create : ControllerBase
    {
        public record Command(string RoomName, DateTime Starts) : IRequest<Unit>;


        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DormitoryDbContext _db;

            public Handler(DormitoryDbContext db) => _db = db;

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var reservation = await _db.Reservations
                    .Where(r => r.RoomName == command.RoomName)
                    .Where(r => r.Starts == command.Starts)
                    .ToListAsync(cancellationToken);

                if (reservation.Count != 0)
                {
                    return Unit.Value;
                }

                await _db.Reservations.AddAsync(
                    new Reservation() {RoomName = command.RoomName, Starts = DateTime.SpecifyKind(command.Starts, DateTimeKind.Utc)}, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
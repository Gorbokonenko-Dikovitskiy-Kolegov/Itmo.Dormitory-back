using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Itmo.Dormitory.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Commands
{
    [ApiExplorerSettings(GroupName = "Reservations")]
    public class Remove : ControllerBase
    {
        public record Command(int Id) : IRequest<Unit>;


        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DormitoryDbContext _db;

            public Handler(DormitoryDbContext db) => _db = db;

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var reservation = await _db.Reservations
                    .Where(r => r.Id == command.Id)
                    .SingleAsync(cancellationToken);

                _db.Reservations.Remove(reservation);
                await _db.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
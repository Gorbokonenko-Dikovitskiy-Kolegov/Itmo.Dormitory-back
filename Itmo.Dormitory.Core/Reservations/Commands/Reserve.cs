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
    public class Reserve : ControllerBase
    {
        public record Command(int Id) : IListRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly DormitoryDbContext _db;

            public Handler(DormitoryDbContext db) => _db = db;

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                var reservation = await _db.Reservations
                    .Where(r => r.Id == command.Id)
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
}
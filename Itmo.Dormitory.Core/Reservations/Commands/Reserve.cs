using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Itmo.Dormitory.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Commands
{
    public static class Reserve
    {
        public record Command(int Id, string ISUNumber) : IListRequest<Result>;
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotNull().GreaterThan(0);
                RuleFor(x => x.ISUNumber).NotEmpty().Matches(@"^\d+$");
            }
        }

        public record Result(bool ReserveSuccessful);

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly DormitoryDbContext _db;

            public Handler(DormitoryDbContext db) => _db = db;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var reservation = await _db.Reservations
                    .Where(r => r.Id == command.Id)
                    .Where(r => r.Reserved == false)
                    .SingleOrDefaultAsync(cancellationToken);

                if (reservation is null)
                {
                    return new Result(false);
                }

                reservation.Reserved = true;
                reservation.Owner = command.ISUNumber;
                _db.Reservations.Update(reservation);
                await _db.SaveChangesAsync(cancellationToken);
                
                return new Result(true);
            }
        }
    }
}
using Itmo.Dormitory.Common.Exceptions;
using Itmo.Dormitory.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Residents.Commands
{
    public static class DeleteResident
    {
        [PublicAPI]
        public record Command(Guid Id) : IRequest;

        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly DormitoryDbContext _dormitoryDbContext;

            public CommandHandler(DormitoryDbContext dormitoryDbContext)
            {
                _dormitoryDbContext = dormitoryDbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var resident = await _dormitoryDbContext.Residents.SingleOrDefaultAsync(
                    t => t.Id == request.Id, cancellationToken);

                if (resident is null)
                    throw new EntityNotFoundException($"Teacher with Id {request.Id} not found");

                _dormitoryDbContext.Residents.Remove(resident);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

using Itmo.Dormitory_backend.Common.Exceptions;
using Itmo.Dormitory_backend.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory_backend.Core.Applications.Commands
{
    public static class DeleteApplication
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
                var application = await _dormitoryDbContext.Applications.SingleOrDefaultAsync(
                    t => t.Id == request.Id, cancellationToken);

                if (application is null)
                    throw new EntityNotFoundException($"Application with Id {request.Id} not found");

                _dormitoryDbContext.Applications.Remove(application);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

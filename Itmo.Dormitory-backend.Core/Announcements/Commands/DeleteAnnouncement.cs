using Itmo.Dormitory_backend.Common.Exceptions;
using Itmo.Dormitory_backend.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory_backend.Core.Announcements.Commands
{
    public static class DeleteAnnouncement
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
                var announcement = await _dormitoryDbContext.Announcements.SingleOrDefaultAsync(
                    t => t.Id == request.Id, cancellationToken);

                if (announcement is null)
                    throw new EntityNotFoundException($"Announcement with Id {request.Id} not found");

                _dormitoryDbContext.Announcements.Remove(announcement);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

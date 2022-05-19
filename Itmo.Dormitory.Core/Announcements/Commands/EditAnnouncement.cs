using FluentValidation;
using Itmo.Dormitory.Common.Exceptions;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Itmo.Dormitory.Core.Announcements.Commands
{
    public static class EditAnnouncement
    {
        [PublicAPI]
        public record Command(
                Guid Id,
                DateTime LastUpdateTime,
                string? Title,
                string Description) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.LastUpdateTime).NotEmpty();
                RuleFor(x => x.Title).MaximumLength(60);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
            }
        }

        [PublicAPI]
        public record Response(
            Guid Id,
            DateTime LastUpdateTime,
            string? Title);

        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly DormitoryDbContext _dormitoryDbContext;

            public CommandHandler(DormitoryDbContext dormitoryDbContext)
            {
                _dormitoryDbContext = dormitoryDbContext;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var announcement = await _dormitoryDbContext.Announcements.SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (announcement is null)
                {
                    throw new EntityNotFoundException($"Announcement with Id {request.Id} not found");
                }                    

                announcement.Information = new AttachedInformation(request.Title, request.Description);
                announcement.LastUpdateTime = new DateTime(request.LastUpdateTime.Year, request.LastUpdateTime.Month,
                                                           request.LastUpdateTime.Day, request.LastUpdateTime.Hour, 
                                                           request.LastUpdateTime.Minute, request.LastUpdateTime.Second);

                _dormitoryDbContext.Announcements.Update(announcement);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return new Response(announcement.Id, announcement.LastUpdateTime, announcement.Information.Title);
            }
        }
    }
}

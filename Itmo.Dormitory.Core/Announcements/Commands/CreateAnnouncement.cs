using FluentValidation;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using Itmo.Dormitory.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Itmo.Dormitory.Core.Announcements.Commands
{
    public static class CreateAnnouncement
    {
        [PublicAPI]
        public record Command(
            DateTime CreateTime,
            string Title,
            string Description) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CreateTime).NotEmpty();
                RuleFor(x => x.Title).MaximumLength(60);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
            }
        }

        [PublicAPI]
        public record Response(
            Guid Id,
            DateTime LastUpdateTime,
            string Title);

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
                var announcement = new Announcement(
                    Guid.NewGuid(),
                    new DateTime(request.CreateTime.Year, request.CreateTime.Month,
                                 request.CreateTime.Day, request.CreateTime.Hour, 
                                 request.CreateTime.Minute, request.CreateTime.Second),

                    new DateTime(request.CreateTime.Year, request.CreateTime.Month, 
                                 request.CreateTime.Day, request.CreateTime.Hour,
                                 request.CreateTime.Minute, request.CreateTime.Second),

                    new AttachedInformation(
                        request.Title, 
                        request.Description));

                await _dormitoryDbContext.Announcements.AddAsync(announcement, cancellationToken);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return new Response(announcement.Id, announcement.LastUpdateTime, announcement.Information.Title);
            }
        }
    }
}

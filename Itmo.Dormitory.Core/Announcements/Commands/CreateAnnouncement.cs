using FluentValidation;
using Itmo.Dormitory.Core.SignalR;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using Itmo.Dormitory.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.SignalR;
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
            private readonly IHubContext<DormitoryHub> _hubContext;

            public CommandHandler(DormitoryDbContext dormitoryDbContext, IHubContext<DormitoryHub> hubContext)
            {
                _dormitoryDbContext = dormitoryDbContext;
                _hubContext = hubContext;
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

                int savedEnitites = await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                if (savedEnitites != 0)
                {
                    await _hubContext.Clients.All.SendAsync("Notify", $"{announcement.Information.Title}", cancellationToken: cancellationToken);
                }                  

                return new Response(announcement.Id, announcement.LastUpdateTime, announcement.Information.Title);
            }
        }
    }
}

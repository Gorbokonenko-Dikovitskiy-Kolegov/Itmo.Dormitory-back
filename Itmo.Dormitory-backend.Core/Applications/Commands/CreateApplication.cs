using FluentValidation;
using Itmo.Dormitory_backend.Common.Exceptions;
using Itmo.Dormitory_backend.DataAccess;
using Itmo.Dormitory_backend.Domain.Entities;
using Itmo.Dormitory_backend.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory_backend.Core.Applications.Commands
{
    public static class CreateApplication
    {
        [PublicAPI]
        public record Command(
            Guid ResidentId,
            DateTime LastUpdateTime,
            string Title,
            string Description,
            ApplicationType ApplicationType) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.LastUpdateTime).NotEmpty();
                RuleFor(x => x.Title).MaximumLength(60);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
                RuleFor(x => x.ApplicationType).NotEmpty();
            }
        }

        [PublicAPI]
        public record Response(
            Guid Id,
            DateTime LastUpdateTime,
            string Resident,
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
                var resident = await _dormitoryDbContext.Residents.Include(c => c.Applications).FirstOrDefaultAsync(c => c.Id == request.ResidentId, cancellationToken);

                if (resident == null) 
                    throw new EntityNotFoundException($"Resident with Id {request.ResidentId} not found");

                var application = new Application(
                    Guid.NewGuid(),
                    new DateTime(request.LastUpdateTime.Year, request.LastUpdateTime.Month,
                                 request.LastUpdateTime.Day, request.LastUpdateTime.Hour,
                                 request.LastUpdateTime.Minute, request.LastUpdateTime.Second),
                    request.ApplicationType,
                    new AttachedInformation(request.Title, request.Description),
                    resident
                    );;
                resident.AddApplication(application);

                await _dormitoryDbContext.Applications.AddAsync(application, cancellationToken);
                _dormitoryDbContext.Update(resident);
                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return new Response(application.Id, application.LastUpdateTime, application.Resident.Name.LastNameAndInitials, application.Information.Title);
            }
        }
    }
}

using FluentValidation;
using Itmo.Dormitory.Common.Exceptions;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Applications.Commands
{
    public static class EditApplication
    {
        [PublicAPI]
        public record Command(
                Guid Id,
                DateTime LastUpdateTime,
                string? Title,
                string Description,
                bool IsResolved) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).MaximumLength(60);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
            }
        }

        [PublicAPI]
        public record Response(
            Guid Id,
            DateTime LastUpdateTime,
            string Resident,
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
                var application = await _dormitoryDbContext.Applications.SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (application is null)
                    throw new EntityNotFoundException($"Application with Id {request.Id} not found");

                application.Information = new AttachedInformation(request.Title, request.Description);
                application.LastUpdateTime = new DateTime(request.LastUpdateTime.Year, request.LastUpdateTime.Month,
                                                           request.LastUpdateTime.Day, request.LastUpdateTime.Hour,
                                                           request.LastUpdateTime.Minute, request.LastUpdateTime.Second);
                application.IsResolved = request.IsResolved;

                _dormitoryDbContext.Applications.Update(application);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return new Response(application.Id, application.LastUpdateTime, application.Resident.Name.LastNameAndInitials, application.Information.Title);
            }
        }
    }
}

using FluentValidation;
using Itmo.Dormitory_backend.Common.Exceptions;
using Itmo.Dormitory_backend.DataAccess;
using Itmo.Dormitory_backend.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory_backend.Core.Residents.Commands
{
    public static class EditResident
    {
        public record Command(
           Guid Id,
           string FirstName,
           string LastName,
           string? MiddleName,
           string ISUNumber,
           string RoomNumber) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.ISUNumber).NotEmpty().Matches(@"^\d+$");
                RuleFor(x => x.RoomNumber).NotEmpty().Matches(@"^\d+$");
            }
        }

        [PublicAPI]
        public record Response(
            Guid Id,
            string LastNameAndInitials);

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
                var resident = await _dormitoryDbContext.Residents.SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (resident is null)
                    throw new EntityNotFoundException($"Resident with Id {request.Id} not found");

                resident.Name = new PersonName(request.FirstName, request.LastName, request.LastName);
                resident.ISUNumber = new ISUNumber(request.ISUNumber);
                resident.RoomNumber = new RoomNumber(request.RoomNumber);

                _dormitoryDbContext.Residents.Update(resident);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return new Response(resident.Id, resident.Name.LastNameAndInitials);
            }
        }
    }
}

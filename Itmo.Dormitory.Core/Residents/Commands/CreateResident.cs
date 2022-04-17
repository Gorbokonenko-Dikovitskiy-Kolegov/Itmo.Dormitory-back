using FluentValidation;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using Itmo.Dormitory.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Residents.Commands
{
    public static class CreateResident
    {
        [PublicAPI]
        public record Command(
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
                var resident = new Resident(
                    Guid.NewGuid(),
                    new PersonName(
                        request.FirstName,
                        request.LastName,
                        request.MiddleName),
                    new ISUNumber(request.ISUNumber),
                    new RoomNumber(request.RoomNumber));

                await _dormitoryDbContext.Residents.AddAsync(resident, cancellationToken);

                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);

                return new Response(resident.Id, resident.Name.LastNameAndInitials);
            }
        }
    }
}

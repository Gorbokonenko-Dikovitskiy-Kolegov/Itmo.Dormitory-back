using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using Itmo.Dormitory.Web.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dormitory.Core.Reservations.Commands
{
    public static class CreateSlot
    {
        public record Command(string RoomName, DateTime Starts) : IRequest<Response>;

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.RoomName).InRange(RoomName.All);
                RuleFor(c => c.Starts).GreaterThan(DateTime.Now);
            }
        }

        public record Response
        {
            public bool Success { get; init; }
        }

        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly DormitoryDbContext _db;

            public CommandHandler(DormitoryDbContext db) => _db = db;

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                var reservation = await _db.Reservations
                    .Where(r => r.RoomName == command.RoomName)
                    .Where(r => r.Starts == command.Starts)
                    .ToListAsync(cancellationToken);

                if (reservation.Count != 0)
                {
                    return new Response() {Success = false};
                }

                await _db.Reservations.AddAsync(
                    new Reservation() {RoomName = command.RoomName, Starts = command.Starts}, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return new Response() {Success = true};
            }
        }
    }
}
using FluentValidation;
using Itmo.Dormitory.DataAccess;
using Itmo.Dormitory.Domain.Entities;
using Itmo.Dormitory.Domain.ValueObjects;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Users.Commands
{
    public static class CreateUser
    {
        [PublicAPI]
        public record Command(
            string ISUNumber,
            string Password): IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ISUNumber).Matches(@"^\d+$");
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        [PublicAPI]
        public record Response(
            string Id);

        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly UserManager<IdentityUser> _userManager;

            public CommandHandler(DormitoryDbContext dormitoryDbContext, UserManager<IdentityUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new IdentityUser()
                {
                    UserName = request.ISUNumber,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                return new Response(result.Succeeded ? user.Id : "Fail");
            }
        }
    }
}

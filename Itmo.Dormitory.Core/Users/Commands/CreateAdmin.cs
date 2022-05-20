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
    public static class CreateAdmin
    {
        [PublicAPI]
        public record Command(
            string Email,
            string Password) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
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

            public CommandHandler(UserManager<IdentityUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new IdentityUser()
                {
                    UserName = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                return new Response(user.Id);
            }
        }
    }
}

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

namespace Itmo.Dormitory.Core.Roles.Commands
{
    public static class CreateRole
    {
        [PublicAPI]
        public record Command(string RoleName) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RoleName).NotEmpty();
            }
        }

        [PublicAPI]
        public record Response(bool isSuccess);
 
        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly DormitoryDbContext _dormitoryDbContext;
            private readonly RoleManager<IdentityRole> _roleManager;

            public CommandHandler(DormitoryDbContext dormitoryDbContext, RoleManager<IdentityRole> roleManager)
            {
                _dormitoryDbContext = dormitoryDbContext;
                _roleManager = roleManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(request.RoleName));

                return new Response(result.Succeeded);
            }
        
        }
        
    }
}

using Itmo.Dormitory.DataAccess;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.Activities.Commands
{
    public static class Refresh
    {
        [PublicAPI]
        public record Command() : IRequest;

        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly DormitoryDbContext _dormitoryDbContext;

            public CommandHandler(DormitoryDbContext dormitoryDbContext)
            {
                _dormitoryDbContext = dormitoryDbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _dormitoryDbContext.Counters.ForEachAsync(el => el.CurrentCount = 0, cancellationToken);
                await _dormitoryDbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}

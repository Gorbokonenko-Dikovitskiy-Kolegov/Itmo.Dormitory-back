using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Dormitory.Core.Jobs;

public class UpdateReservations : JobDb
{
    public UpdateReservations(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }

    protected override Task<bool> DoDbJob(IServiceScope scope, CancellationToken cancellationToken)
    {
        var reservations = Db.Reservations.Where(r => r.Starts < DateTime.Now);

        foreach (var reservation in reservations)
        {
            Db.Reservations.Remove(reservation);
        }

        return Task.FromResult(true);
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Itmo.Dormitory.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Dormitory.Core.Jobs;

public abstract class JobDb : Job
{
    private readonly IServiceScopeFactory _scopeFactory;

    protected JobDb(IServiceScopeFactory scopeFactory, int intervalMin = 1) : base(intervalMin)
    {
        _scopeFactory = scopeFactory;
    }

    protected DormitoryDbContext Db { get; private set; }

    protected abstract Task<bool> DoDbJob(IServiceScope scope, CancellationToken cancellationToken);

    public override async Task DoJob(CancellationToken cancellationToken)
    {
        while (true)
        {
            using var scope = _scopeFactory.CreateScope();
            try
            {
                Db = scope.ServiceProvider.GetRequiredService<DormitoryDbContext>();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to connect to database");
                await Task.Delay(TimeSpan.FromMinutes(_intervalMin), cancellationToken);
                continue;
            }
            
            try
            {
                await DoDbJob(scope, cancellationToken);
                await Db.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                Console.WriteLine("Job {0} has failed", this.GetType().Name);
                await Task.Delay(TimeSpan.FromMinutes(_intervalMin), cancellationToken);
            }
            
            await Task.Delay(TimeSpan.FromMinutes(_intervalMin), cancellationToken);

            await Db.DisposeAsync();
        }
    }
}
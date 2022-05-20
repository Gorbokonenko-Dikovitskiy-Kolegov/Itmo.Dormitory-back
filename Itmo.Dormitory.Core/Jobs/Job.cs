using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Itmo.Dormitory.Core.Jobs
{
    public abstract class Job : BackgroundService
    {
        private readonly string _name;
        protected readonly int _intervalMin;

        protected Job(int intervalMin)
        {
            _name = GetType().Name;
            _intervalMin = intervalMin;
        }


        protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await JobInternal(stoppingToken);
        }

        private async Task JobInternal(CancellationToken cancellationToken)
        {
            Console.WriteLine("Job {0} has started", _name);

            while (true)
            {
                try
                {
                    await DoJob(cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Job {0} has failed unexpectedly", _name);

                    await Task.Delay(TimeSpan.FromMinutes(_intervalMin), cancellationToken);

                    Console.WriteLine("Job {0} is restarting now", _name);
                }
                
                await Task.Delay(TimeSpan.FromMinutes(_intervalMin), cancellationToken);
            }
        }

        public abstract Task DoJob(CancellationToken stoppingToken);
    }
}
using Infrastructure.Persistences.Seeders;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Workers;

public class InitializeSeedWorker(IServiceProvider serviceProvider) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        IdentitySeeder.Seed(serviceProvider);
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
using ConsumerService.Domain.Interfaces;

namespace ConsumerService.API;

public class Worker(IServiceProvider serviceProvider, ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            using var scope = serviceProvider.CreateScope();
            var messageQueueService = scope.ServiceProvider.GetRequiredService<IMessageQueueService>();
            await messageQueueService.StartConsuming(stoppingToken);
        }
    }
}
namespace ConsumerService.Domain.Interfaces;

public interface IMessageQueueService
{
    Task StartConsuming(CancellationToken cancellationToken);
}
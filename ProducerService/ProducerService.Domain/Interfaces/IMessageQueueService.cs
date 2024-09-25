namespace ProducerService.Domain.Interfaces;

public interface IMessageQueueService
{
    void Publish(string message);
}
using ProducerService.Domain.Interfaces;

namespace ProducerService.Infrastructure.Services;

public class MessageService(IMessageQueueService messageQueueService)
{
    public void SendMessage(string message)
    {
        messageQueueService.Publish(message);
    }
}
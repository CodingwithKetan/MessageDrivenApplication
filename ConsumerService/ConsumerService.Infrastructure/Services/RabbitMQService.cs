using System.Text;
using ConsumerService.Domain.Interfaces;
using ConsumerService.Infrastructure.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerService.Infrastructure.Services;

public class RabbitMQService(IMessageHandler messageHandler, IConnection connection, IModel channel) : IMessageQueueService
{
    
    public async Task StartConsuming(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(channel);
        channel.QueueDeclare(queue: "producer-consumer",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            messageHandler.ProcessMessage(message);
        };

        channel.BasicConsume(queue: "producer-consumer",
            autoAck: true,
            consumer: consumer);
        await Task.Delay(-1, cancellationToken);
    }
}
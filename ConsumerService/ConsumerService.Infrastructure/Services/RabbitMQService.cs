using System.Data.Common;
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
        var consumerTasks = new List<Task>();

        channel.QueueDeclare(queue: "producer-consumer",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        for (int i = 0; i < 5; i++)
        {
            consumerTasks.Add(CreateConsumerAsync(cancellationToken));
        }
        await Task.WhenAll(consumerTasks);
    }

    private Task CreateConsumerAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            int retryCount = ea.BasicProperties.Headers != null &&
            ea.BasicProperties.Headers.ContainsKey("x-retry-count")
                ? (int)ea.BasicProperties.Headers["x-retry-count"]
                : 0;

            try
            {
                messageHandler.ProcessMessage(message);
                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch(Exception e)
            {
                retryCount++;

                if (retryCount <= 3)
                {
                    var properties = channel.CreateBasicProperties();
                    properties.Headers = new Dictionary<string, object>
                    {
                        { "x-retry-count", retryCount }
                    };
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "producer-consumer",
                        basicProperties: properties,
                        body: body);
                }
                else
                {
                    Console.WriteLine($"Message processing failed after {retryCount} attempts. Discarding message.");
                }
            }
        };

        channel.BasicConsume(queue: "producer-consumer",
            autoAck: false,             
            consumer: consumer);

        return Task.Delay(Timeout.Infinite, cancellationToken);
    }
}
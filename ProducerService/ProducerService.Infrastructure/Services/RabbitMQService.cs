using System.Text;
using ProducerService.Domain.Interfaces;
using RabbitMQ.Client;

namespace ProducerService.Infrastructure.Services;

public class RabbitMqService: IMessageQueueService
{
    private readonly ConnectionFactory _factory = new() { HostName = "localhost" };

    public void Publish(string message)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "producer-consumer",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
            routingKey: "producer-consumer",
            basicProperties: null,
            body: body);
    }
}
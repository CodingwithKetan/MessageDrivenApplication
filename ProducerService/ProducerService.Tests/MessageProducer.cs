using System.Text;
using RabbitMQ.Client;

namespace ProducerService.Tests;

public class MessageProducer
{
    private readonly IConnection _connection;

    public MessageProducer(IConnection connection)
    {
        _connection = connection;
    }

    public async Task SendMessageAsync(string message)
    {
        using (var channel = _connection.CreateModel())
        {
            channel.QueueDeclare(queue: "test_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: "test_queue",
                basicProperties: null,
                body: body);

            await Task.CompletedTask;
        }
    }
}
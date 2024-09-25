using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProducerService.Tests;

public class RabbitMQIntegrationTests
{
    private IConnection _connection;
    private IModel _channel;
    
    public RabbitMQIntegrationTests()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "test_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }
    
    [Fact]
    public async Task SendMessage_ReceivesMessage()
    {
        
        var messageProducer = new MessageProducer(_connection);
        var message = "Integration testing";
        var tcs = new TaskCompletionSource<string>();
        
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var receivedMessage = Encoding.UTF8.GetString(body);
            tcs.SetResult(receivedMessage);
        };
        
        _channel.BasicConsume(queue: "test_queue", autoAck: true, consumer: consumer);

        await messageProducer.SendMessageAsync(message);
        
        var receivedMessage = await tcs.Task;
        Assert.Equal(message, receivedMessage);
    }
}
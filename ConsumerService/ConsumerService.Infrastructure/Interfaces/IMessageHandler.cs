namespace ConsumerService.Infrastructure.Interfaces;

public interface IMessageHandler
{
    void ProcessMessage(string message);
}
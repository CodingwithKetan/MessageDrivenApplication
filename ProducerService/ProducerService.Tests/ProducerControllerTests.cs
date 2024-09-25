using Microsoft.AspNetCore.Mvc;
using Moq;
using ProducerService.Api.Controllers;
using ProducerService.Domain.Interfaces;
using ProducerService.Infrastructure.Services;

namespace ProducerService.Tests;

public class ProducerControllerTests
{
    [Fact]
    public void ProcessMessage_Should_ProcessMessage()
    {
        var mockMessageQueueService = new Mock<IMessageQueueService>();
        var messageService = new MessageService(mockMessageQueueService.Object);
        var controller = new ProducerController(messageService);

        var result = controller.ProcessMessage("Trying to process message, hope it will process.....");

        Assert.IsType<OkResult>(result);
        mockMessageQueueService.Verify(m => m.Publish(It.IsAny<string>()), Times.Once);
    }
}
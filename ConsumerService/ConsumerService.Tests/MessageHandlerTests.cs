using ConsumerService.Infrastructure;
using ConsumerService.Infrastructure.Interfaces;
using Moq;

namespace ConsumerService.Tests;

public class MessageHandlerTests
{
    
    [Fact]
    public void ProcessMessage_ShouldProcessMessageSuccessFully_When_LegitMessageRecieved()
    {
        var mockLogService = new Mock<IProcessingResultService>();
        var messageProcessor = new MessageHandler(mockLogService.Object);
        var messageToProcess = "Sending Message to Process....";

        messageProcessor.ProcessMessage(messageToProcess);

        mockLogService.Verify(l => l.MarkAsSuccessfullyProcessed(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
    }
    
    [Fact]
    public void ProcessMessage_Should_MarkMessage_As_Failed_When_Message_Is_Blank()
    {
        var mockLogService = new Mock<IProcessingResultService>();
        var messageProcessor = new MessageHandler(mockLogService.Object);
        var message = "";

        messageProcessor.ProcessMessage(message);

        mockLogService.Verify(l => l.MarkAsFailedToProcess(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
    }
}
using ConsumerService.Domain.Enums;
using ConsumerService.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConsumerService.Tests;

public class ProcessingResultServiceTests
{
    [Fact]
    public void MarkAsSuccessfullyProcessed_Should_Record_MessageProcessingStatus_As_SuccessfullyProcessed()
    {
        var mockLogger = new Mock<ILogger<ProcessingResultService>>();
        var logService = new ProcessingResultService(mockLogger.Object);
        var message = "Tada, Message is process Successfully";

        logService.MarkAsSuccessfullyProcessed(message, DateTime.Now, DateTime.Now.AddMilliseconds(1000));

        var logs = logService.GetProcessingResult().ToList();
        Assert.Single(logs);
        Assert.Equal(MessageProcessingStatus.Success, logs.First().ProcessingStatus);
        Assert.Equal(message, logs.First().Message);
    }
    
    [Fact]
    public void LogError_ShouldLogInformationAndAddLogEntry()
    {
        var mockLogger = new Mock<ILogger<ProcessingResultService>>();
        var processingResultService = new ProcessingResultService(mockLogger.Object);
        var message = "Hard Luck, Unable to Process Message";

        processingResultService.MarkAsFailedToProcess(message, "Invalid Message",DateTime.Now, DateTime.Now.AddMilliseconds(1000));

        var processingResult = processingResultService.GetProcessingResult().ToList();
        Assert.Single(processingResult);
        Assert.Equal(MessageProcessingStatus.Error, processingResult.First().ProcessingStatus);
        Assert.Equal(message, processingResult.First().Message);
    }
}
using ConsumerService.Domain.Enums;
using ConsumerService.Domain.Model;
using ConsumerService.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConsumerService.Infrastructure.Services;

public class ProcessingResultService(ILogger<ProcessingResultService> logger) : IProcessingResultService
{
    private readonly List<MessageProcessingResult> processingResult = [];
    public void MarkAsSuccessfullyProcessed(string message,DateTime processingStartTime, DateTime processingEndTime)
    {
        logger.LogInformation(message);
        RecordProcessingResult(message, MessageProcessingStatus.Success, processingStartTime, processingEndTime);
    }

    public void MarkAsFailedToProcess(string message, string errorMessage, DateTime processingStartTime, DateTime processingEndTime)
    {
        logger.LogInformation(message);
        RecordProcessingResult(message, MessageProcessingStatus.Error, processingStartTime, processingEndTime, errorMessage);
    }

    private void RecordProcessingResult(string message, MessageProcessingStatus processingStatus, DateTime processingStartTime, DateTime processingEndTime, string errorMessage = "")
    {
        var processingResult = new MessageProcessingResult
        {
            Message = message,
            ProcessingStatus = processingStatus,
            ProcessingStartTime = processingStartTime,
            ProcessingEndTime = processingEndTime,
            ErrorMessage = errorMessage
        };
        this.processingResult.Add(processingResult);
    }

    public IEnumerable<MessageProcessingResult> GetProcessingResult()
    {
        return processingResult;
    }

    public IEnumerable<MessageProcessingResult> GetMessagesMarkedAsFailed()
    {
        return processingResult.Where(_ => _.ProcessingStatus == MessageProcessingStatus.Error);
    }

    public IEnumerable<MessageProcessingResult> GetSuccessfullyProcessedMessage()
    {
        return processingResult.Where(_ => _.ProcessingStatus == MessageProcessingStatus.Success);
    }

    public IEnumerable<MessageProcessingResult> GetProcessingResult(DateTime startDate, DateTime endDate)
    {
        return processingResult.Where(_ => _.ProcessingEndTime >= startDate && _.ProcessingEndTime <= endDate);
    }
}
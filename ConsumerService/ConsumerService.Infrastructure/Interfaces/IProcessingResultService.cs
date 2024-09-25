using ConsumerService.Domain.Model;

namespace ConsumerService.Infrastructure.Interfaces;

public interface IProcessingResultService
{
    void MarkAsSuccessfullyProcessed(string message, DateTime processingStartTime, DateTime processingEndTime);
    void MarkAsFailedToProcess(string message, string errorMessage, DateTime processingStartTime, DateTime processingEndTime);
    IEnumerable<MessageProcessingResult> GetProcessingResult();
    IEnumerable<MessageProcessingResult> GetProcessingResult(DateTime startDate, DateTime endDate);
    IEnumerable<MessageProcessingResult> GetMessagesMarkedAsFailed();
    IEnumerable<MessageProcessingResult> GetSuccessfullyProcessedMessage();
}
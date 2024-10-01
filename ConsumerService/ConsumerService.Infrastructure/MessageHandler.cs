using ConsumerService.Infrastructure.Interfaces;

namespace ConsumerService.Infrastructure;

public class MessageHandler(IProcessingResultService processingResultService) : IMessageHandler
{
    const int MILLI_SECOND = 1000;
    public void ProcessMessage(string message)
    {
        DateTime processingStartTime = DateTime.Now;
        Thread.Sleep(GetMessageProcessingTimeInSec() * MILLI_SECOND);
        try
        {
            if (string.IsNullOrEmpty(message?.Trim()))
            {
                throw new Exception("Invalid message");
            }

            processingResultService.MarkAsSuccessfullyProcessed(message, processingStartTime, DateTime.Now);
        }
        catch (Exception ex)
        {
            processingResultService.MarkAsFailedToProcess(message, ex.Message, processingStartTime, DateTime.Now);
            throw;
        }
    }

    private static int GetMessageProcessingTimeInSec()
    {
        Random random = new Random();
        return random.Next(1, 6);
    }
}
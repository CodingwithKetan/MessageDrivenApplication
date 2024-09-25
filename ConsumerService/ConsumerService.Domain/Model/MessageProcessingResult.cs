using ConsumerService.Domain.Enums;

namespace ConsumerService.Domain.Model;

public class MessageProcessingResult
{
    public string Message { get; set; }
    public MessageProcessingStatus ProcessingStatus { get; set; }
    public DateTime ProcessingStartTime { get; set; }
    public DateTime ProcessingEndTime { get; set; }
    public string ErrorMessage {  get; set; }
}
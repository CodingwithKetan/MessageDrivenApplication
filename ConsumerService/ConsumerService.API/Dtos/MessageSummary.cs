namespace ConsumerService.API.Dtos
{
    public class MessageSummaryDTO
    {
        public MessageSummaryDTO(int successCount, int failureCount)
        {
            SuccessCount = successCount;
            FailureCount = failureCount;    
        }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }

}

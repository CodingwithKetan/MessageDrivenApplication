using ConsumerService.API.Dtos;
using ConsumerService.Domain.Model;
using ConsumerService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageProcessingResultController(IProcessingResultService processingResultService) : ControllerBase
{
    [HttpGet("Summary")]
    public ActionResult<MessageSummaryDTO> GetAllProcessedMessageResult()
    {
        var numberOfSuccessfullyProcessedMessage = processingResultService.GetSuccessfullyProcessedMessage().Count();
        var numberOfMessagesProcessedToFailed = processingResultService.GetMessagesMarkedAsFailed().Count();

        return Ok(new MessageSummaryDTO(numberOfSuccessfullyProcessedMessage, numberOfMessagesProcessedToFailed));
    }

    [HttpGet("Success")]
    public ActionResult<MessageProcessingResult> GetSuccessfullyProcessedMessages()
    {
        var messages = processingResultService.GetSuccessfullyProcessedMessage();
        return Ok(messages);
    }

    [HttpGet("Failed")]
    public ActionResult<MessageProcessingResult> GetFailedToProcessMessages()
    {
        var messages = processingResultService.GetMessagesMarkedAsFailed();
        return Ok(messages);
    }

    [HttpGet("Processed-Messages")]
    public IActionResult GetProcessedMessages(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequest("Start date must be before end date.");
        }

        var results = processingResultService.GetProcessingResult(startDate, endDate);

        if (!results.Any())
        {
            return NotFound("No messages found in the specified date range.");
        }

        return Ok(results);
    }


}
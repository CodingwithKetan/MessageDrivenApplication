using Microsoft.AspNetCore.Mvc;
using ProducerService.Infrastructure.Services;

namespace ProducerService.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProducerController(MessageService messageService) : ControllerBase
{
    [HttpPost]
    public IActionResult ProcessMessage([FromBody] string message)
    {
        messageService.SendMessage(message);
        return Ok();
    }
}
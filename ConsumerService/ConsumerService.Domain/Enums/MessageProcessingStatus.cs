using System.ComponentModel.DataAnnotations;

namespace ConsumerService.Domain.Enums;

public enum MessageProcessingStatus
{
    [Display(Name = "Success")]
    Success,
    [Display(Name = "Error")]
    Error
}
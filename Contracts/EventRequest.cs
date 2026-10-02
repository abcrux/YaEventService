using System.ComponentModel.DataAnnotations;

namespace YaEventService.Contracts;

public class EventRequest
{
    [Required(ErrorMessage = "Название события обязательно")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
    public required string Title { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Время начала обязательно")]
    public required DateTime StartAt { get; set; }

    [Required(ErrorMessage = "Время окончания обязательно")]
    public required DateTime EndAt { get; set; }

}



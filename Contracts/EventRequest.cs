using System.ComponentModel.DataAnnotations;

namespace YaEventService.Contracts;

public class EventRequest : IValidatableObject
{
    [Required(ErrorMessage = "Название события обязательно")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
    public required string Title { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Время начала обязательно")]
    public required DateTime StartAt { get; set; }

    [Required(ErrorMessage = "Время окончания обязательно")]
    public required DateTime EndAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndAt <= StartAt)
        {
            // Ошибка к полю StartAt
            yield return new ValidationResult("Начало должно быть раньше окончания", new[] { nameof(StartAt) });
            // Ошибка к полю EndAt
            yield return new ValidationResult("Окончание должно быть позже начала",  new[] { nameof(EndAt) });
        }
    }

}



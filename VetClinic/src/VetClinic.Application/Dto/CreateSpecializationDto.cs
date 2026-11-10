using VetClinic.Domain.Enums;

namespace VetClinic.Application.DTOs;

/// <summary>
/// Данные для создания или обновления специализации
/// </summary>
public class CreateSpecializationDto
{
    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Профильный вид животных
    /// </summary>
    public AnimalSpecies TargetSpecies { get; set; }
}
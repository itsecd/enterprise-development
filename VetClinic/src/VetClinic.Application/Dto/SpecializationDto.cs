using VetClinic.Domain.Enums;

namespace VetClinic.Application.DTOs;

/// <summary>
/// DTO специализации
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Профильный вид животных
    /// </summary>
    public AnimalSpecies TargetSpecies { get; set; }
}
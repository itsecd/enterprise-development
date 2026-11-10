using VetClinic.Domain.Enums;

namespace VetClinic.Application.DTOs;

/// <summary>
/// DTO породы
/// </summary>
public class BreedDto
{
    /// <summary>
    /// Идентификатор породы
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название породы
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    public AnimalSpecies Species { get; set; }
}
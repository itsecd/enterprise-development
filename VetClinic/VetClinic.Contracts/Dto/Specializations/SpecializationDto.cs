using VetClinic.Shared;

namespace VetClinic.Contracts.Dto.Specializations;

/// <summary>
/// Специализация врача
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Вид животного, с которым работает специализация, null если специализация общая
    /// </summary>
    public AnimalSpecies? FocusSpecies { get; init; }
}

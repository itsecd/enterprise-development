using VetClinic.Shared;

namespace VetClinic.Contracts.Dto.Breeds;

/// <summary>
/// Порода животного
/// </summary>
public class BreedDto
{
    /// <summary>
    /// Идентификатор породы
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Название породы
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Биологический вид животного
    /// </summary>
    public required AnimalSpecies Species { get; init; }
}

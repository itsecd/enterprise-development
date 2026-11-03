using VetClinic.Shared;

namespace VetClinic.Contracts.Dto.Pets;

/// <summary>
/// Питомец
/// </summary>
public class PetDto
{
    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Кличка питомца
    /// </summary>
    public required string Nickname { get; init; }

    /// <summary>
    /// Биологический вид животного
    /// </summary>
    public required AnimalSpecies Species { get; init; }

    /// <summary>
    /// Идентификатор породы, null если порода не указана
    /// </summary>
    public int? BreedId { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateTime BirthDate { get; init; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    public required double Weight { get; init; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public required int OwnerId { get; init; }
}

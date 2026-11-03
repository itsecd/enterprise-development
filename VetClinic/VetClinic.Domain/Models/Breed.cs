using VetClinic.Shared;

namespace VetClinic.Domain.Models;

/// <summary>
/// Порода животного
/// </summary>
public class Breed
{
    /// <summary>
    /// Уникальный идентификатор породы
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название породы
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Биологический вид животного
    /// </summary>
    public AnimalSpecies Species { get; set; }
}

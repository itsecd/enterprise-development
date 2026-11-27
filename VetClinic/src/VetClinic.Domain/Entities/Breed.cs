using VetClinic.Domain.Enums;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Domain.Entities;

/// <summary>
/// Порода животного
/// </summary>
public class Breed : IEntity
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
    /// Биологический вид животного, к которому относится порода
    /// </summary>
    public AnimalSpecies Species { get; set; }
}
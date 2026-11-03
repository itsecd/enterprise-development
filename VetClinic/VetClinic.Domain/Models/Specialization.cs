using VetClinic.Shared;

namespace VetClinic.Domain.Models;

/// <summary>
/// Специализация врача
/// </summary>
public class Specialization
{
    /// <summary>
    /// Уникальный идентификатор специализации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вид животного, с которым работает специализация, null если специализация общая
    /// </summary>
    public AnimalSpecies? FocusSpecies { get; set; }
}

using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Специализация ветеринара
/// </summary>
public class Specialization
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Вид животных для специализации
    /// </summary>
    public AnimalType AnimalType { get; set; }
}

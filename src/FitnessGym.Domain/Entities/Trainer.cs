namespace FitnessGym.Domain.Entities;

/// <summary>
/// Тренер фитнес-клуба.
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Идентификатор специализации.
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Специализация (справочная сущность).
    /// </summary>
    public Specialization Specialization { get; set; } = null!;

    /// <summary>
    /// Стаж работы в годах.
    /// </summary>
    public required int ExperienceYears { get; set; }
}

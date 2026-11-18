namespace FitnessClub.Domain.Entities;

/// <summary>
/// Класс тренера
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Идентификатор специализации (внешний ключ)
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Специализация тренера
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы
    /// </summary>
    public int ExperienceYears { get; set; }
}

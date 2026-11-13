namespace FitnessClub.Domain.Entities;

/// <summary>
/// Тренер фитнес-клуба.
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Специализация тренера.
    /// </summary>
    public Specialization Specialization { get; set; } = null!;

    /// <summary>Идентификатор связанной сущности Specialization.</summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Стаж работы тренера в годах.
    /// </summary>
    public int WorkExperience { get; set; }
}
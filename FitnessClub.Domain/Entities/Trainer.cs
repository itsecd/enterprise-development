namespace FitnessClub.Domain.Entities;

/// <summary>
/// Тренер фитнес-клуба.
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Специализация тренера.
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы в годах.
    /// </summary>
    public int WorkExperience { get; set; }
}
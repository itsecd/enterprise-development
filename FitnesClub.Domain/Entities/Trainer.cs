namespace FitnesClub.Domain.Entities;

/// <summary>
/// Тренер фитнес-клуба
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Идентификатор специализации тренера
    /// </summary>
    public required Guid SpecializationId { get; set; }

    /// <summary>
    /// Специализация тренера
    /// </summary>
    public Specialization? Specialization { get; set; }

    /// <summary>
    /// Стаж работы тренера в годах
    /// </summary>
    public required int WorkExperienceYears { get; set; }

    /// <summary>
    /// Записи клиентов на занятия с тренером.
    /// </summary>
    public List<Booking> Bookings { get; set; } = [];
}
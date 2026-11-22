using BeautySalon.Shared.Enums;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Мастер салона красоты
/// </summary>
public class Master : Person
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы (лет)
    /// </summary>
    public int WorkExperience { get; set; }
}
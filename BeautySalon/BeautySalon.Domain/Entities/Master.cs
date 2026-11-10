namespace BeautySalon.Domain.Entities;

/// <summary>
/// Мастер
/// </summary>
public class Master : Person
{

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Стаж работы
    /// </summary>
    public required int WorkExperience { get; set; }
}

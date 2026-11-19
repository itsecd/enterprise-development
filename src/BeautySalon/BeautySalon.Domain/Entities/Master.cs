namespace BeautySalon.Domain.Entities;

/// <summary>
/// Класс мастера, реализующий класс Person.
/// </summary>
public class Master : Person
{
    /// <summary>
    /// Номер паспорта мастера.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Специализация мастера.
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Стаж работы мастера.
    /// </summary>
    public required int ExperienceYears { get; set; }
}

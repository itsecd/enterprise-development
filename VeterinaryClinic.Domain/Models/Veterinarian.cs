namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Ветеринар клиники
/// </summary>
public class Veterinarian : Person
{
    /// <summary>
    /// Идентификатор ветеринара
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта ветеринара
    /// </summary>
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения ветеринара
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Специализация ветеринара и вид животных
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public int ExperienceYears { get; set; }
}

namespace VetClinic.Domain.Models;

/// <summary>
/// Ветеринарный врач
/// </summary>
public class Vet
{
    /// <summary>
    /// Уникальный идентификатор врача
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>
    public int BirthYear { get; set; }

    /// <summary>
    /// Специализация врача
    /// </summary>
    public int? SpecializationId { get; set; }

    public Specialization? Specialization { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public int WorkExperienceYears { get; set; }
}

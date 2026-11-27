using VetClinic.Domain.Interfaces;

namespace VetClinic.Domain.Entities;

/// <summary>
/// Ветеринар
/// </summary>
public class Veterinarian : IEntity
{
    /// <summary>
    /// Уникальный идентификатор врача
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Серия и номер паспорта врача
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество врача
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Год рождения врача
    /// </summary>
    public int BirthYear { get; set; }

    /// <summary>
    /// Идентификатор специализации врача
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Специализация врача
    /// </summary>
    public Specialization? Specialization { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Список приемов, закрепленных за данным врачом
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];
}
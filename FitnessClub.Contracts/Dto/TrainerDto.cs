using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Тренер (read-модель)
/// </summary>
public class TrainerDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public string PassportNumber { get; init; } = string.Empty;

    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; init; }

    /// <summary>
    /// Специализация
    /// </summary>
    public SpecializationDto Specialization { get; init; } = new();

    /// <summary>
    /// Стаж работы (лет)
    /// </summary>
    public int ExperienceYears { get; init; }
}
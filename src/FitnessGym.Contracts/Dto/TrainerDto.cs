using FitnessGym.Domain.Enums;

namespace FitnessGym.Contracts.Dto;

/// <summary>
/// DTO тренера фитнес-клуба: данные в ответе сервера.
/// </summary>
public class TrainerDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public string PassportNumber { get; init; } = string.Empty;

    /// <summary>
    /// ФИО.
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Пол.
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly BirthDate { get; init; }

    /// <summary>
    /// Идентификатор специализации тренера (ссылка на справочник).
    /// </summary>
    public int SpecializationId { get; init; }

    /// <summary>
    /// Название специализации; заполняется только в ответах сервера.
    /// </summary>
    public string SpecializationName { get; init; } = string.Empty;

    /// <summary>
    /// Стаж работы в годах.
    /// </summary>
    public int ExperienceYears { get; init; }
}

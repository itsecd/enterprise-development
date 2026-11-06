using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Базовый класс для клиента и тренера фитнес-клуба.
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}
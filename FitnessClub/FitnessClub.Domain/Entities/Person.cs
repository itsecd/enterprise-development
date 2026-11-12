using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Базовый класс для человека, являющегося участником фитнес-клуба.
/// </summary>
public abstract class Person : IEntity
{
/// <summary>
/// ID человека.
/// </summary>
public int Id { get; set; }

    /// <summary>
    /// Номер паспорта человека.
    /// </summary>
    /// <example>1234 567890</example>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО.
    /// </summary>
    /// <example>Иванов Иван Иванович</example>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public required DateTime BirthDate { get; set; }
}
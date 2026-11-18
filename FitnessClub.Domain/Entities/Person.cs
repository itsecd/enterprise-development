using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Базовый класс человека
/// </summary>
public abstract class Person : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО (полностью)
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateTime BirthDate { get; set; }
}

using FitnessGym.Domain.Enums;

namespace FitnessGym.Domain.Entities;

/// <summary>
/// Базовый класс для клиента и тренера
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия Имя Отчество
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}

using FitnesClub.Domain.Enums;

namespace FitnesClub.Domain.Entities;

/// <summary>
/// Базовый класс для клиента и тренера фитнес-клуба.
/// </summary>
public abstract class Person
{
    /// <summary>
    /// ID 
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    /// <example>+79998887766</example>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    /// <example>ivan@test.com</example>
    public string? Email { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}
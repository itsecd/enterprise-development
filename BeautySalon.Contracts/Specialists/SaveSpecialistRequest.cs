using System.ComponentModel.DataAnnotations;
using BeautySalon.Domain.Shared.Enums;

namespace BeautySalon.Contracts.Specialists;

/// <summary>
/// Данные специалиста для создания или обновления
/// </summary>
public sealed class SaveSpecialistRequest
{
    /// <summary>
    /// Фамилия
    /// </summary>
    [Required]
    [StringLength(100)]
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Имя
    /// </summary>
    [Required]
    [StringLength(100)]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Отчество при наличии
    /// </summary>
    [StringLength(100)]
    public string? Patronymic { get; init; }

    /// <summary>
    /// Пол специалиста
    /// </summary>
    [Required]
    [EnumDataType(typeof(Gender))]
    public Gender? Gender { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Required]
    public DateOnly? DateOfBirth { get; init; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    [Phone]
    [StringLength(30)]
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Паспортные данные при наличии
    /// </summary>
    [StringLength(20)]
    public string? PassportNumber { get; init; }

    /// <summary>
    /// Специализация
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Specialization { get; init; } = string.Empty;

    /// <summary>
    /// Стаж работы в полных годах
    /// </summary>
    [Required]
    [Range(0, 100)]
    public int? WorkExperience { get; init; }
}
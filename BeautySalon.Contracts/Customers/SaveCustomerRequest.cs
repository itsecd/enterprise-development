using System.ComponentModel.DataAnnotations;
using BeautySalon.Domain.Shared.Enums;

namespace BeautySalon.Contracts.Customers;

/// <summary>
/// Данные посетителя для создания или обновления
/// </summary>
public sealed class SaveCustomerRequest
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
    /// Пол посетителя
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
    [Required]
    [Phone]
    [StringLength(30)]
    public string PhoneNumber { get; init; } = string.Empty;
}
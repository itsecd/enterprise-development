using System.ComponentModel.DataAnnotations;

namespace Hotel.Contracts.Request;

/// <summary>
/// Обработка запросов на создание/обновление клиента
/// </summary>
public sealed class ClientRequest
{
    /// <summary>
    /// Фамилия
    /// </summary>
    [Required, StringLength(100)]
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Имя
    /// </summary>
    [Required, StringLength(100)]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Отчество (при наличии)
    /// </summary>
    [StringLength(100)]
    public string? Patronymic { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Required]
    public DateTime? BirthDate { get; init; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Required, StringLength(20)]
    public string NumberPassport { get; init; } = string.Empty;

    /// <summary>
    /// Гражданство
    /// </summary>
    [Required, StringLength(50)]
    public string Citizenship { get; init; } = string.Empty;
}
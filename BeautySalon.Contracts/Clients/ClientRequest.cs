using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Clients;

/// <summary>
/// Запрос на создание или обновление клиента.
/// </summary>
public class ClientRequest
{
    /// <summary>
    /// ФИО.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public required string Phone { get; set; }
}
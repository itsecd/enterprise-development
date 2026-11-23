using BeautySalon.Domain.Enums;

namespace BeautySalon.Contracts.Clients;

/// <summary>
/// Данные клиента.
/// </summary>
public class ClientResponse
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int Id { get; set; }

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
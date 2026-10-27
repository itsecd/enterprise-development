namespace Hotel.Domain.Entities;

/// <summary>
/// Клиент гостиницы
/// </summary>
public class HotelClient
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта клиента
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Имя клиента
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия клиента
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Отчество клиента
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Дата рождения клиента
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Гражданство клиента
    /// </summary>
    public required string Citizenship { get; set; }
}

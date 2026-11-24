namespace HotelSystem.Application.Contracts.Clients;

/// <summary>
/// Контракт ответа с информацией о клиенте
/// </summary>
public class ClientResponse : IEntityResponse
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; init; }

    /// <summary>
    /// Гражданство
    /// </summary>
    public required string Citizenship { get; init; }
}
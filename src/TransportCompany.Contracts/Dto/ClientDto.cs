namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO клиента транспортной компании
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// ФИО клиента или наименование организации
    /// </summary>
    /// <example>Иванов Иван Иванович</example>
    /// <example>ООО "Тмыв"</example>
    public required string Name { get; init; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? Phone { get; init; }
}
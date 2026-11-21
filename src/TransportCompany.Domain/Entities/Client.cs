namespace TransportCompany.Domain.Entities;

/// <summary>
/// Клиент транспортной компании
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО клиента или Наименование организации
    /// </summary>
    /// <example>Иванов Иван Иванович</example>
    /// <example>ООО "Рога и копыта"</example>
    public required string Name { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? Phone { get; set; }
}
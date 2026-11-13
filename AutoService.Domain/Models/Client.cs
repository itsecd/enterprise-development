namespace AutoService.Domain.Models;

/// <summary>
/// Информация о клиенте
/// </summary>
public class Client
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Телефон клиента
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Список авто клиента
    /// </summary>
    public List<Car> Cars { get; set; } = [];
}
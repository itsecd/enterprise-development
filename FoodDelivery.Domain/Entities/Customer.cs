namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Клиент
/// </summary>
public class Customer : IEntity
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Фамилия клиента
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя клиента
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество клиента (может отсутствовать)
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Адрес доставки клиента
    /// </summary>
    public required string DeliveryAddress { get; set; }
}
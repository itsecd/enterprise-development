namespace FoodDelivery.Domain;

/// <summary>
/// Клиент службы доставки еды
/// </summary>
public class Customer
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
    /// Отчество клиента
    /// </summary>
    public required string Patronymic { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Адрес доставки клиента
    /// </summary>
    public required string DeliveryAddress { get; set; }
}
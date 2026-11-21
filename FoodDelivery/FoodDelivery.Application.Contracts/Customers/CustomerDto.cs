namespace FoodDelivery.Application.Contracts.Customers;

/// <summary>
/// Сведения о клиенте службы доставки
/// </summary>
public sealed class CustomerDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Фамилия клиента
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Имя клиента
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Отчество клиента
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string Phone { get; init; }

    /// <summary>
    /// Адрес доставки клиента
    /// </summary>
    public required string DeliveryAddress { get; init; }
}

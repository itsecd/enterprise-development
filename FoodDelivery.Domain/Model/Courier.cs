using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Model;

/// <summary>
/// Курьер службы доставки
/// </summary>
public class Courier
{
    /// <summary>
    /// Идентификатор курьера
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО курьера
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона курьера
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Тип транспорта курьера
    /// </summary>
    public required TransportType Transport { get; set; }

    /// <summary>
    /// Заказы курьера
    /// </summary>
    public List<Order> Orders { get; set; } = [];
}

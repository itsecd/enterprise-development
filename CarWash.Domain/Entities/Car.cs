using CarWash.Domain.Shared.Enums;

namespace CarWash.Domain.Entities;

/// <summary>
/// Автомобиль клиента
/// </summary>
public class Car
{
    /// <summary>
    /// Уникальный идентификатор автомобиля
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Категория автомобиля
    /// </summary>
    public CarCategory CarCategory { get; set; }

    /// <summary>
    /// Государственный регистрационный номер автомобиля
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Идентификатор владельца автомобиля
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Владелец автомобиля
    /// </summary>
    public required CarWashCustomer Customer { get; set; }

    /// <summary>
    /// Заказы на обслуживание автомобиля
    /// </summary>
    public List<Order> Orders { get; set; } = [];
}
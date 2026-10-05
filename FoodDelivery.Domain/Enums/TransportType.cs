namespace FoodDelivery.Domain.Enums;

/// <summary>
/// Способ доставки курьером
/// </summary>
public enum TransportType
{
    /// <summary>
    /// Доставка пешком
    /// </summary>
    Foot,

    /// <summary>
    /// Доставка на велосипеде
    /// </summary>
    Bicycle,

    /// <summary>
    /// Доставка на скутере
    /// </summary>
    Scooter,

    /// <summary>
    /// Доставка на машине
    /// </summary>
    Car
}
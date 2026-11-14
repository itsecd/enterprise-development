namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Ресторан службы доставки еды.
/// </summary>
public class Restaurant : IEntity
{
    /// <summary>
    /// Идентификатор ресторана.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название ресторана.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес ресторана.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Рейтинг ресторана.
    /// </summary>
    public required double Rating { get; set; }

    /// <summary>
    /// Время открытия ресторана.
    /// </summary>
    public required TimeOnly OpenTime { get; set; }

    /// <summary>
    /// Время закрытия ресторана.
    /// </summary>
    public required TimeOnly CloseTime { get; set; }
}
namespace FoodDelivery.Domain.Models;

/// <summary>
/// Ресторан, который принимает заказы
/// </summary>
public class Restaurant
{
    /// <summary>
    /// Идентификатор ресторана
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название ресторана
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес ресторана
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Рейтинг ресторана
    /// </summary>
    public required decimal Rating { get; set; }

    /// <summary>
    /// Время открытия ресторана
    /// </summary>
    public required TimeSpan OpeningTime { get; set; }

    /// <summary>
    /// Время закрытия ресторана
    /// </summary>
    public required TimeSpan ClosingTime { get; set; }
}
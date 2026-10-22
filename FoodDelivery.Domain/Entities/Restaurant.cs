namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Ресторан, из которого можно заказывать
/// </summary>
public class Restaurant : IEntity
{
    /// <summary>
    /// Уникальный идентификатор ресторана
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
    public required double Rating { get; set; }

    /// <summary>
    /// Время открытия ресторана
    /// </summary>
    public required TimeOnly OpeningTime { get; set; }

    /// <summary>
    /// Время закрытия ресторана
    /// </summary>
    public required TimeOnly ClosingTime { get; set; }

    /// <summary>
    /// Список блюд ресторана
    /// </summary>
    public List<Dish> Dishes { get; set; } = [];
}
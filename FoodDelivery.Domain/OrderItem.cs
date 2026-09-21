namespace FoodDelivery.Domain;

/// <summary>
/// Отдельная позиция в заказе
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Уникальный идентификатор позиции заказа
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public required int OrderId { get; set; }

    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public required int DishId { get; set; }

    /// <summary>
    /// Блюдо, добавленное в заказ
    /// </summary>
    public required Dish Dish { get; set; }

    /// <summary>
    /// Количество единиц блюда
    /// </summary>
    public required int Quantity { get; set; }

    /// <summary>
    /// Цена одной единицы блюда на момент оформления заказа
    /// </summary>
    public required decimal UnitPrice { get; set; }

    /// <summary>
    /// Заказ, в который входит позиция
    /// </summary>
    public required Order Order { get; set; }
}
namespace FoodDelivery.Domain;
/// <summary>
/// Позиция в заказе
/// </summary>
public class OrderItem
{
    /// <summary>
    /// ID позиции
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ID заказа, в котором есть эта позиция
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// ID блюда
    /// </summary>
    public int DishId { get; set; }
    /// <summary>
    /// Ссылка на блюдо
    /// </summary>
    public required Dish Dish { get; set; }
    /// <summary>
    /// Количество блюд в позиции
    /// </summary>
    public int Quantity { get; set; } 
}

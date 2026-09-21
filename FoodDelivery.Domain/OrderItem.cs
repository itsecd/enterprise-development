namespace FoodDelivery.Domain;

/// <summary>
/// Represents a dish included in a food delivery order
/// </summary>
public class OrderItem
{
    public required int Id { get; set; }

    public required int OrderId { get; set; }

    public required int DishId { get; set; }

    public required int Quantity { get; set; }

    public required decimal UnitPrice { get; set; }

    public required Order Order { get; set; }

    public required Dish Dish { get; set; }
}
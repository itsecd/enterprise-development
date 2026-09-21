namespace FoodDelivery.Domain;

/// <summary>
/// Represents a food delivery order placed by a customer
/// </summary>
public class Order
{
    public required int Id { get; set; }

    public required int CustomerId { get; set; }

    public required int RestaurantId { get; set; }

    public required DateTime OrderTime { get; set; }

    public required DateTime DeliveryTime { get; set; }

    public required decimal TotalAmount { get; set; }

    public required Customer Customer { get; set; }

    public required Restaurant Restaurant { get; set; }

    public List<OrderItem> Items { get; set; } = [];
}
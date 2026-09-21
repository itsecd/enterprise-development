namespace FoodDelivery.Domain;

/// <summary>
/// Represents a dish that can be included in an order
/// </summary>
public class Dish
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required int Weight { get; set; }

    public required decimal Price { get; set; }

    public required int CategoryId { get; set; }

    public required Category Category { get; set; }
}
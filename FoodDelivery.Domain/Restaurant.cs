namespace FoodDelivery.Domain;

/// <summary>
/// Represents a restaurant that accepts food delivery orders
/// </summary>
public class Restaurant
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public required decimal Rating { get; set; }

    public required TimeOnly OpeningTime { get; set; }

    public required TimeOnly ClosingTime { get; set; }
}
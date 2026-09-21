namespace FoodDelivery.Domain;

/// <summary>
/// Represents a category of dishes offered by restaurants
/// </summary>
public class Category
{
    public required int Id { get; set; }

    public required string Name { get; set; }
}
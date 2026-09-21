namespace FoodDelivery.Domain;

/// <summary>
/// Represents a customer who can place food delivery orders
/// </summary>
public class Customer
{
    public required int Id { get; set; }

    public required string LastName { get; set; }

    public required string FirstName { get; set; }

    public required string Patronymic { get; set; }

    public required string Phone { get; set; }

    public required string DeliveryAddress { get; set; }
}
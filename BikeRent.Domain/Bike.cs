namespace BikeRent.Domain;
/// <summary>
/// Bike avalible for rent
/// </summary>
public class Bike
{
    /// <summary>
    /// Bike's id
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Bike's type's id
    /// </summary>
    public required int TypeId { get; set; }
    /// <summary>
    /// Bike's model
    /// </summary>
    public required string Model { get; set; }
    /// <summary>
    /// Bike's color
    /// </summary>
    public required string Color { get; set; }
}

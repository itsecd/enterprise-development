namespace CarWash.Domain;

/// <summary>
/// Represents a vehicle with its registration number, brand, and category.
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Gets the registration number of the vehicle.
    /// </summary>
    public required string GovNumber { get; init; }

    /// <summary>
    /// Gets the brand of the vehicle.
    /// </summary>
    public required string Brand { get; init; }

    /// <summary>
    /// Gets the category of the vehicle.
    /// </summary>
    public required VehicleCategory Category { get; init; }
}

namespace CarWash.Domain;

/// <summary>
/// Represents a service offered by the car wash, including its name, price, duration, and applicable vehicle category.
/// </summary>
public class Service
{
    /// <summary>
    /// Gets or sets the name of the service.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the price of the service.
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the duration of the service in minutes.
    /// </summary>
    public required int DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the vehicle category for which the service is applicable.
    /// </summary>
    public required VehicleCategory Category { get; set; }
}

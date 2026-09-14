namespace CarWash.Domain;

/// <summary>
/// Represents an order placed by a client for a specific vehicle and service at the car wash, including the scheduled start time and box number.
/// </summary>
public class Order
{
    /// <summary>
    /// Gets the client who placed the order.
    /// </summary>
    public required Client Client { get; init; }

    /// <summary>
    /// Gets the vehicle for which the order is placed.
    /// </summary>
    public required Vehicle Vehicle { get; init; }

    /// <summary>
    /// Gets the service for which the order is placed.
    /// </summary>
    public required Service Service { get; init; }

    /// <summary>
    /// Gets the date and time when the service is scheduled to start.
    /// </summary>
    public required DateTime ServiceStartTime { get; init; }

    /// <summary>
    /// Gets the box number assigned to the order.
    /// </summary>
    public required int BoxNumber { get; init; }
}

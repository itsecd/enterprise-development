namespace CarWash.Domain;

/// <summary>
/// Represents a client of the car wash, including their full name, phone number, and a list of vehicles they own.
/// </summary>
public class Client
{
    /// <summary>
    /// Gets or sets the full name of the client.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the client.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the list of vehicles owned by the client.
    /// </summary>
    public required List<Vehicle> Vehicles { get; set; }
}

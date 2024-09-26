namespace HotelBookingSystem.Domain;

/// <summary>
/// Hotel
/// </summary>
public class Hotel
{
    /// <summary>
    /// Hotel ID
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Hotel name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// City where the hotel is located
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Hotel address
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Hotel rooms
    /// </summary>
    public List<Room> Rooms { get; set; } = [];
}
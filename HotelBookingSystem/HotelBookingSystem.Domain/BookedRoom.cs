namespace HotelBookingSystem.Domain;

/// <summary>
/// Booked room
/// </summary>
public class BookedRoom
{
    /// <summary>
    /// Booked room ID
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Client ID
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Hotel room ID
    /// </summary>
    public required int RoomId { get; set; }

    /// <summary>
    /// Date of entry
    /// </summary>
    public required DateOnly EntryDate { get; set; }

    /// <summary>
    /// Departure date
    /// </summary>
    public required DateOnly DepartureDate { get; set; }

    /// <summary>
    /// Reservation period
    /// </summary>
    public required TimeSpan BookingPeriod { get; set; }
}
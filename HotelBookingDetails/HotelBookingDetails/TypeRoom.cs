namespace HotelBookingDetails.Domain;

/// <summary>
/// Типы номеров
/// </summary>
public class TypeRoom
{
    /// <summary>
    /// Идентификатор типа
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название типа
    /// </summary>
    public required string Name { get; set; }
}

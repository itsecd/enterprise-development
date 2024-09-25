namespace HotelBookingDetails.Domain;

/// <summary>
///     Типы номеров
/// </summary>
public class TypeRoom
{

    /// <summary>
    ///     Индентификатор типа
    /// </summary>
    public required int id { get; set; }


    /// <summary>
    ///     Название типа
    /// </summary>
    public required string NameType { get; set; }
}

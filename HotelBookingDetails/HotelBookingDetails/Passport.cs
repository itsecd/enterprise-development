namespace HotelBookingDetails.Domain;

/// <summary>
/// Паспорт
/// </summary>
public struct Passport
{
    /// <summary>
    /// Идентификатор паспорта
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required int Number { get; set; }

    /// <summary>
    /// Серия паспорта
    /// </summary>
    public required int Series { get; set; }
}

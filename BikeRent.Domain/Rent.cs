namespace BikeRent.Domain;
/// <summary>
/// Аренда велосипеда
/// </summary>
public class Rent
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; set; }
    /// <summary>
    /// Идентификатор велосипеда
    /// </summary>
    public required int BikeId { get; set; }
    /// <summary>
    /// Начало аренды
    /// </summary>
    public required DateTime Begin { get; set; }
    /// <summary>
    /// Конец аренды
    /// </summary>
    public required DateTime End { get; set; }
}
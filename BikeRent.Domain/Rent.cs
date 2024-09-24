namespace BikeRent.Domain;
/// <summary>
/// Аренда велосипеда
/// </summary>
public class Rent
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    required public int ClientId { get; set; }
    /// <summary>
    /// Идентификатор велосипеда
    /// </summary>
    required public int BikeId { get; set; }
    /// <summary>
    /// Начало аренды
    /// </summary>
    required public DateTime Begin { get; set; }
    /// <summary>
    /// Конец аренды
    /// </summary>
    required public DateTime End { get; set; }
}
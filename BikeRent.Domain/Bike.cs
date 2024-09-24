namespace BikeRent.Domain;
/// <summary>
/// Велосипед
/// </summary>
public class Bike
{
    /// <summary>
    /// Идентификатор велосипеда
    /// </summary>
    public int BikeId { get; set; }
    /// <summary>
    /// идентификатор типа велосипеда
    /// </summary>
    public int TypeId { get; set; }
}

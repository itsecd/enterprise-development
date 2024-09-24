namespace BikeRent.Domain;
/// <summary>
/// Велосипед
/// </summary>
public class Bike
{
    /// <summary>
    /// Идентификатор велосипеда
    /// </summary>
    required public int BikeId { get; set; }
    /// <summary>
    /// идентификатор типа велосипеда
    /// </summary>
    required public int TypeId { get; set; }
    /// <summary>
    /// Модель
    /// </summary>
    required public string Model { get; set; }
    /// <summary>
    /// Цвет
    /// </summary>
    required public string Color { get; set; }
}

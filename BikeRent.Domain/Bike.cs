namespace BikeRent.Domain;
/// <summary>
/// Велосипед
/// </summary>
public class Bike
{
    /// <summary>
    /// Идентификатор велосипеда
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Идентификатор типа велосипеда
    /// </summary>
    public required int TypeId { get; set; }
    /// <summary>
    /// Модель
    /// </summary>
    public required string Model { get; set; }
    /// <summary>
    /// Цвет
    /// </summary>
    public required string Color { get; set; }
}

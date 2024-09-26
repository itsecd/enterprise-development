namespace BikeRent.Domain;
/// <summary>
/// Class that represents specific bike types
/// </summary>
public class BikeType
{
    /// <summary>
    /// Type's id
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Rental cost
    /// </summary>
    public required double Price { get; set; }
    /// <summary>
    /// Type's name
    /// </summary>
    public required string Name { get; set; }
}
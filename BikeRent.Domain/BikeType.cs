namespace BikeRent.Domain;
/// <summary>
/// Class that represents specific bike types
/// </summary>
public class BikeType
{
    /// <summary>
    /// Type's id
    /// </summary>
    required public int Id { get; set; }
    /// <summary>
    /// Rental cost
    /// </summary>
    required public double Price { get; set; }
    /// <summary>
    /// Type's name
    /// </summary>
    required public string Name { get; set; }
}
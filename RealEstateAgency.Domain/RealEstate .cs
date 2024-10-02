namespace RealEstateAgency.Domain;

/// <summary>
/// объект недвижимости
/// </summary>
public class RealEstate
{
    public enum PropertyType
    {
        Residential,
        Uninhabitable
    }

    /// <summary>
    /// тип объекта недвижимости (жилое/нежилое)
    /// </summary>
    public PropertyType Type { get; set; }

    /// <summary>
    /// адресс
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// площадь
    /// </summary>
    public double Square { get; set; }

    /// <summary>
    /// количество комнат
    /// </summary>
    public int NumberOfRooms { get; set; }
}

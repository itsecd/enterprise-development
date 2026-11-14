namespace CarWash.Domain.Entities;

/// <summary>
/// Автомобиль клиента
/// </summary>
public class Car
{
    /// <summary>
    /// ID авто
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Гос. рег. номер
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка авто
    /// </summary>
    public string? Brand { get; set; }
}
namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные автомобиля
/// </summary>
public class CarDto
{
    /// <summary>
    /// ID автомобиля
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Государственный регистрационный номер
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public string? Brand { get; set; }
}

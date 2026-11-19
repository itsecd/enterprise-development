namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для передачи данных об автомобиле.
/// </summary>
public class CarDto
{
    /// <summary>
    /// Идентификатор автомобиля.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Государственный номер.
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля.
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Модель автомобиля.
    /// </summary>
    public required string Model { get; set; }

    /// <summary>
    /// Год выпуска.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Идентификатор владельца автомобиля.
    /// </summary>
    public Guid ClientId { get; set; }
}

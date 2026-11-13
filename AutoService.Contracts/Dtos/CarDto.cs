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
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Марка автомобиля.
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Модель автомобиля.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Год выпуска.
    /// </summary>
    public int Year { get; set; }
}
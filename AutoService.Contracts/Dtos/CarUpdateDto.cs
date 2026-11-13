namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных автомобиля.
/// </summary>
public class CarUpdateDto
{
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
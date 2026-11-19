namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных автомобиля.
/// </summary>
public class CarUpdateDto
{
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

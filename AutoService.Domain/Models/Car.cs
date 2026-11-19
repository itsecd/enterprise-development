namespace AutoService.Domain.Models;

/// <summary>
/// Характеристики автомобиля
/// </summary>
public class Car
{
    /// <summary>
    /// Уникальный идентификатор авто
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Государственный номер
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Модель автомобиля
    /// </summary>
    public required string Model { get; set; }
    
    /// <summary>
    /// Год выпуска
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Идентификатор клиента, которому принадлежит авто
    /// </summary>
    public Guid ClientId { get; set; }
}
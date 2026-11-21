using CarWash.Shared.Enums;

namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные услуги
/// </summary>
public class ServiceDto
{
    /// <summary>
    /// ID услуги
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория автомобиля, для которого предназначена услуга
    /// </summary>
    public CarCategory Category { get; set; }

    /// <summary>
    /// Стоимость услуги в рублях
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    public int DurationMinutes { get; set; }
}

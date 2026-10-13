using CarWash.Domain.Enums;

namespace CarWash.Domain.Entities;

/// <summary>
/// Услуга автомойки
/// </summary>
public class Service
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
    /// Продолжительность выполнения услуги в минутах
    /// </summary>
    public int DurationMinutes { get; set; }
}
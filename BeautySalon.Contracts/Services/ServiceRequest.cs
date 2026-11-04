namespace BeautySalon.Contracts.Services;

/// <summary>
/// Запрос на создание или обновление услуги.
/// </summary>
public class ServiceRequest
{
    /// <summary>
    /// Название услуги.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги.
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// Стоимость услуги.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Длительность услуги.
    /// </summary>
    public TimeSpan Duration { get; set; }
}
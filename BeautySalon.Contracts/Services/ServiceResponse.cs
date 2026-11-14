namespace BeautySalon.Contracts.Services;

/// <summary>
/// Данные услуги.
/// </summary>
public class ServiceResponse
{
    /// <summary>
    /// Идентификатор услуги.
    /// </summary>
    public int Id { get; set; }

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
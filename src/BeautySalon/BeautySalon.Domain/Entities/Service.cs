namespace BeautySalon.Domain.Entities;

/// <summary>
/// Класс услуги.
/// </summary>
public class Service
{
    /// <summary>
    /// Уникальный идентификатор услуги.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название услуги.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги.
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// Цена услуги.
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Длительность услуги.
    /// </summary>
    public required TimeSpan Duration { get; set; }
}

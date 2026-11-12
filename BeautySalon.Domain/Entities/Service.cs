namespace BeautySalon.Domain.Entities;

/// <summary>
/// Услуга салона красоты
/// </summary>
public class Service
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Стоимость услуги
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    public int DurationMinutes { get; set; }
}

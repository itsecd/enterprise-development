namespace BeautySalon.Domain.Entities;

/// <summary>
/// Услуга
/// </summary>
public class Service
{

    /// <summary>
    /// Идентификатор услуги 
    /// </summary>
    public required int Id {  get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Длительность
    /// </summary>
    public required int Duration { get; set; }
}

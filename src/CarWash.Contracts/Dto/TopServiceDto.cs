namespace CarWash.Contracts.Dto;

/// <summary>
/// Услуга и количество её заказов
/// </summary>
public class TopServiceDto
{
    /// <summary>
    /// Данные услуги
    /// </summary>
    public required ServiceDto Service { get; set; }

    /// <summary>
    /// Количество заказов услуги
    /// </summary>
    public int UsageCount { get; set; }
}

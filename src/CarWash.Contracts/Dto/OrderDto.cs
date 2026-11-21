namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные заказа
/// </summary>
public class OrderDto
{
    /// <summary>
    /// ID заказа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required ClientDto Client { get; set; }

    /// <summary>
    /// Автомобиль
    /// </summary>
    public required CarDto Car { get; set; }

    /// <summary>
    /// Услуга
    /// </summary>
    public required ServiceDto Service { get; set; }

    /// <summary>
    /// Дата и время начала обслуживания
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    public int BoxNumber { get; set; }
}

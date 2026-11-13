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
    /// ID клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string ClientFullName { get; set; }

    /// <summary>
    /// ID автомобиля
    /// </summary>
    public int CarId { get; set; }

    /// <summary>
    /// Госномер автомобиля
    /// </summary>
    public required string CarLicensePlate { get; set; }

    /// <summary>
    /// ID услуги
    /// </summary>
    public int ServiceId { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string ServiceName { get; set; }

    /// <summary>
    /// Дата и время начала обслуживания
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    public int BoxNumber { get; set; }
}

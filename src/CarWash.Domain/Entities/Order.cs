namespace CarWash.Domain.Entities;

/// <summary>
/// Заказ (контракт)
/// </summary>
public class Order
{
    /// <summary>
    /// ID заказа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент-заказчик
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Выбранная услуга
    /// </summary>
    public required Service Service { get; set; }

    /// <summary>
    /// Обслуживаемый автомобиль
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Дата и время начала мойки
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    public int BoxNumber { get; set; }
}
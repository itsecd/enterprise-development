namespace CarWash.Domain.Entities;

/// <summary>
/// Заказ на обслуживание автомобиля
/// </summary>
public class Order
{
    /// <summary>
    /// Уникальный идентификатор заказа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public int CarId { get; set; }

    /// <summary>
    /// Автомобиль, обслуживаемый по заказу
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public int ProcedureId { get; set; }

    /// <summary>
    /// Услуга, выполняемая по заказу
    /// </summary>
    public required CarWashProcedure Procedure { get; set; }

    /// <summary>
    /// Дата и время начала обслуживания
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    public int WashBoxNumber { get; set; }
}
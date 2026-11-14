namespace DryCleaning.Domain.Enum;

/// <summary>
/// Статус заказа в химчистке
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Принят
    /// </summary>
    Accepted = 0,

    /// <summary>
    /// В обработке
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Выполнен
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Выдан
    /// </summary>
    Issued = 3,

    /// <summary>
    /// Отменен
    /// </summary>
    Cancelled = 4
}
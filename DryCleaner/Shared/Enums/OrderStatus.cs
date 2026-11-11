namespace DryCleaner.Shared.Enums;

/// <summary>
/// Статусы заказа
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Принят
    /// </summary>
    Accepted = 0,

    /// <summary>
    /// В прогрессе
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Готов
    /// </summary>
    Ready = 2,

    /// <summary>
    /// Отдан
    /// </summary>
    Issued = 3,

    /// <summary>
    /// Отменен
    /// </summary>
    Cancelled = 4
}
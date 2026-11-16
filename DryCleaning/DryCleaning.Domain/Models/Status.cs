namespace DryCleaning.Domain.Models;

/// <summary>
/// Статус обработки заказа
/// </summary>

public enum Status
{
    /// <summary>
    /// Принят
    /// </summary>
    Accept,

    /// <summary>
    /// Исполняется
    /// </summary>
    InProgress,

    /// <summary>
    /// Готов к выдаче
    /// </summary>
    Completed,

    /// <summary>
    /// Выдан
    /// </summary>
    Given

}

namespace DryCleaner.Shared.Enums;

/// <summary>
/// Статус заказа
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// В процессе выполнения
    /// </summary>
    InProgress,
    /// <summary>
    /// Готов к выдаче
    /// </summary>
    Ready,
    /// <summary>
    /// Выдан
    /// </summary>
    Issued,
}
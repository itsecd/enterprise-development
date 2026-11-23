namespace Hotel.Domain.Shared.Enums;

/// <summary>
/// Категории номеров гостиницы
/// </summary>
public enum RoomCategory
{
    /// <summary>
    /// Эконом
    /// </summary>
    Economy = 0,

    /// <summary>
    /// Стандарт
    /// </summary>
    Standart = 1,

    /// <summary>
    /// Комфорт
    /// </summary>
    Comfort = 2,

    /// <summary>
    /// Делюкс
    /// </summary>
    Delux = 3,

    /// <summary>
    /// Сьюит (Люкс)
    /// </summary>
    Suite = 4,

    /// <summary>
    /// Резиденция
    /// </summary>
    Residence = 5
}
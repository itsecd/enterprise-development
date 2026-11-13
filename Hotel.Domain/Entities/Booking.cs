using Hotel.Domain.Interfaces;

namespace Hotel.Domain.Entities;

/// <summary>
/// Бронирование клиентов гостиницы
/// </summary>
public class Booking : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Дата заселения
    /// </summary>
    public required DateTime DateArrival { get; set; }

    /// <summary>
    /// Количество дней проживания клиента
    /// </summary>
    public required int DayCount { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Комната
    /// </summary>
    public required Room Room { get; set; }

}

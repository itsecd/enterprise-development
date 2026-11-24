namespace Hotel.Domain.Models;

/// <summary>
/// Факт заселения клиента в номер
/// </summary>
public class Booking
{
    /// <summary>
    /// Уникальный идентификатор бронирования
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Номер
    /// </summary>
    public required Room Room { get; set; }

    /// <summary>
    /// Дата заезда
    /// </summary>
    public required DateTime CheckInDate { get; set; }

    /// <summary>
    /// Количество суток проживания
    /// </summary>
    public required int Nights { get; set; }
}
namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные бронирования в ответе API
/// </summary>
public class BookingDto
{
    /// <summary>
    /// Идентификатор бронирования
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required ClientDto Client { get; set; }

    /// <summary>
    /// Номер гостиницы
    /// </summary>
    public required RoomDto Room { get; set; }

    /// <summary>
    /// Дата заезда
    /// </summary>
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// Количество суток проживания
    /// </summary>
    public int Nights { get; set; }

    /// <summary>
    /// Дата выезда, не включаемая в период проживания
    /// </summary>
    public DateTime CheckOutDate { get; set; }

    /// <summary>
    /// Полная стоимость проживания по текущему тарифу номера
    /// </summary>
    public decimal TotalPrice { get; set; }
}

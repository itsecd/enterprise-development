namespace Hotel.Domain.Entities;

/// <summary>
/// Информация о бронировании
/// </summary>
public class Booking
{
    /// <summary>
    /// Идентификатор Брони
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Заселяющийся клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Дата заезда
    /// </summary>
    public required DateOnly CheckInDate { get; set; }

    /// <summary>
    /// Колличество дней проживания
    /// </summary>
    public required uint DaysOfStay { get; set; }

    /// <summary>
    /// Бронируемая комната
    /// </summary>
    public required Room Room { get; set; }

    /// <summary>
    /// Итоговая цена проживания
    /// </summary>
    public double TotalPrice => Room.Type.DayPrice * DaysOfStay;
}

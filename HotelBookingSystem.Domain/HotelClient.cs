namespace HotelBookingSystem.Domain;

/// <summary>
/// Клиенты
/// </summary>
public class HotelClient
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The number of passport of the lodger
    /// </summary>
    public required int Passport { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly Birthdate { get; set; }

    /// <summary>
    /// Забронированные клиентом номера
    /// </summary>
    public List<BookedRooms> Brooms { get; set; } = [];

}
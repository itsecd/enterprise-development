namespace HotelBookingDetails.Domain;

/// <summary>
///     Клиент
/// </summary>
public class Client
{
    /// <summary>
    ///     Индентификатор клиента
    /// </summary>
    public required int id { get; set; }
    /// <summary>
    ///     Имя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    ///     Пасспортные данные
    /// </summary>
    public required Passport PassportData { get; set; }

    /// <summary>
    ///     День рождения
    /// </summary>
    public required DateOnly Birthday { get; set; }
}




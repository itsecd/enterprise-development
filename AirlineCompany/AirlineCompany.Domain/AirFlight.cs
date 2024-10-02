namespace AirlineCompany.Domain;
/// <summary>
/// Класс описывает 1 полет какого-либо самолета
/// </summary>
public class AirFlight
{
    /// <summary>
    /// ID полета
    /// </summary>
    public required int Idflight { get; set; }

    /// <summary>
    /// Кодовый номер рейса
    /// </summary>
    public required string CodeNumber { get; set; }

    /// <summary>
    /// Пункт отправления
    /// </summary>
    public required string DeparturePoint { get; set; }

    /// <summary>
    /// Пункт прибытия
    /// </summary>
    public required string ArrivalPoint { get; set; }

    /// <summary>
    /// Дата и время отправления
    /// </summary>
    public required DateTime Departure { get; set; }

    /// <summary>
    /// Дата и время прибытия
    /// </summary>
    public required DateTime Arrive { get; set; }

    /// <summary>
    /// Время в пути в часах и минутах
    /// </summary>
    public required TimeOnly FlyingTime { get; set; }

    /// <summary>
    /// Тип самолета
    /// </summary>
    public required string PlaneType { get; set; }
}


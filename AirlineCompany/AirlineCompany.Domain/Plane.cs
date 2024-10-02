namespace AirlineCompany.Domain;
/// <summary>
/// Класс описывает одну модель самолета
/// </summary>
public class Plane
{
    /// <summary>
    /// ID самолета
    /// </summary>
    public required int IdPlane { get; set; }

    /// <summary>
    /// Модель
    /// </summary>
    public required string Model { set; get; }

    /// <summary>
    /// Грузоподъемность
    /// </summary>
    public required double LoadCapacity { set; get; }

    /// <summary>
    /// Производительность
    /// </summary>
    public required double Efficiency { set; get; }

    /// <summary>
    /// Максимальное число пассажиров
    /// </summary>
    public required int PassengerMax { set; get; }
}


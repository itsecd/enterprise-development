namespace DispatchService.Model;

/// <summary>
/// Транспортное средство
/// </summary>
public class Transport
{
    /// <summary>
    /// Виды транспорта
    /// </summary>
    public enum VehicleType
    {
        Bus,
        Tram,
        Trolleybus
    }

    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Госномер автомобиля
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Вид транспорта
    /// </summary>
    public required VehicleType Type { get; set; }

    /// <summary>
    /// Название модели транспорта
    /// </summary>
    public required string ModelName { get; set; }

    /// <summary>
    /// Тип транспорта (низкопольный / высокопольный) 
    /// </summary>
    public bool? IsLowFloor { get; set; }

    /// <summary>
    /// Максимальная вместительность
    /// </summary>
    public int? MaxCapacity { get; set; }

    /// <summary>
    /// Год выпуска транспорта
    /// </summary>
    public int? YearOfManufacture { get; set; }
}
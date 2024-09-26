namespace DispatchService.Model;

/// <summary>
/// Транспортное средство
/// </summary>
public class Transport
{
    /// <summary>
    /// Список видов транспорта
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
    public int Id { get; set; }

    /// <summary>
    /// Госномер автомобиля
    /// </summary>
    public string LicensePlate { get; set; }

    /// <summary>
    /// Вид транспорта
    /// </summary>
    public VehicleType Type { get; set; }

    /// <summary>
    /// Название модели транспорта
    /// </summary>
    public string ModelName { get; set; }

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

    public Transport(int _ID, string licensePlate, VehicleType _Type, string modelName, bool isLowFloor, int maxCapacity, int yearOfManufacture)
    {
        ID = _ID;
        LicensePlate = licensePlate;
        Type = _Type;
        ModelName = modelName;
        IsLowFloor = isLowFloor;
        MaxCapacity = maxCapacity;
        YearOfManufacture = yearOfManufacture;
    }
}
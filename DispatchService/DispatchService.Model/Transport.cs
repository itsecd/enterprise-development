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
    /// Индентификатор транспорта
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Госномер автомобиля
    /// </summary>
    public string LicensePlate { get; set; }

    /// <summary>
    /// Вид транспорта
    /// </summary>
    private VehicleType type;
    public VehicleType Type
    {
        get { return type; }
        set { type = value; }
    }
    
    /// <summary>
    /// Название модели транспорта
    /// </summary>
    public string ModelName { get; set; }

    /// <summary>
    /// Тип транспорта (низкопольный / ненизкопльный) 
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

    public string TypeDescription
    {
        get
        {
            switch (type)
            {
                case VehicleType.Bus:
                    return "Автобус";
                case VehicleType.Tram:
                    return "Трамвай";
                case VehicleType.Trolleybus:
                    return "Троллейбус";
                default:
                    return null;
            }
        }
    }

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
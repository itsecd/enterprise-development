namespace DispatchTransportControl.Domain;

/// <summary>
/// Транспортное средство
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Гос. номер транспортного средства
    /// </summary>
    public required string RegistrationNumber { get; set; }

    /// <summary>
    /// Тип транспортного средства
    /// </summary>
    public required VehicleType VehicleType { get; set; }

    /// <summary>
    /// Модель транспортного средства
    /// </summary>
    public required VehicleModel VehicleModel { get; set; }

    /// <summary>
    /// Год выпуска транспортного средства
    /// </summary>
    public required int YearOfManufacture { get; set; }
}

/// <summary>
/// Типы транспортных средств
/// </summary>
public enum VehicleType
{
    Bus,
    Trolley,
    Tram
}
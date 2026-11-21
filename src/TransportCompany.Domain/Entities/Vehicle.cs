namespace TransportCompany.Domain.Entities;

/// <summary>
/// Транспортное средство
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Государственный регистрационный номер
    /// </summary>
    /// <example>А123БВ77</example>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Грузоподъёмность в тоннах
    /// </summary>
    public double? LoadCapacity { get; set; }

    /// <summary>
    /// Идентификатор модели
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// Модель транспортного средства
    /// </summary>
    public VehicleModel? Model { get; set; }
}
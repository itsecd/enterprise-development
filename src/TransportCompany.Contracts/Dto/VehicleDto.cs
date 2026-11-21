namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO транспортного средства
/// </summary>
public class VehicleDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Государственный регистрационный номер
    /// </summary>
    /// <example>А123БВ77</example>
    public required string LicensePlate { get; init; }

    /// <summary>
    /// Грузоподъёмность в тоннах
    /// </summary>
    public double? LoadCapacity { get; init; }

    /// <summary>
    /// Идентификатор модели
    /// </summary>
    public required int ModelId { get; init; }
}
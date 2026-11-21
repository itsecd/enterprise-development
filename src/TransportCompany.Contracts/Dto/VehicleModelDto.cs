using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO модели транспортного средства
/// </summary>
public class VehicleModelDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Тип кузова
    /// </summary>
    public required BodyType BodyType { get; init; }

    /// <summary>
    /// Объём кузова в м^3
    /// </summary>
    public double? BodyVolume { get; init; }
}
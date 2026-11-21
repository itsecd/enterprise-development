namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO количества рейсов транспортного средства
/// </summary>
public class VehicleTripCountDto
{
    /// <summary>
    /// Государственный регистрационный номер
    /// </summary>
    /// <example>А123БВ77</example>
    public required string LicensePlate { get; init; }

    /// <summary>
    /// Количество рейсов
    /// </summary>
    public required int TripCount { get; init; }
}
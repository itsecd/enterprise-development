namespace TransportCompany.Domain.Models;

/// <summary>
/// Результат аналитики по количеству рейсов транспортного средства
/// </summary>
public sealed record VehicleTripCount(int VehicleId, string LicensePlate, int TripCount);
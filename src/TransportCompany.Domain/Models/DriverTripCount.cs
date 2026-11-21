namespace TransportCompany.Domain.Models;

/// <summary>
/// Результат аналитики по количеству рейсов водителя
/// </summary>
public sealed record DriverTripCount(int DriverId, string DriverName, int TripCount);
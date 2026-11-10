namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO водителя из топа по количеству рейсов
/// </summary>
public class TopDriverDto
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required int DriverId { get; init; }

    /// <summary>
    /// ФИО водителя
    /// </summary>
    public required string DriverName { get; init; }

    /// <summary>
    /// Количество рейсов
    /// </summary>
    public required int TripCount { get; init; }
}
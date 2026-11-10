using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO рейса транспортной компании
/// </summary>
public class TripDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required int DriverId { get; init; }

    /// <summary>
    /// Идентификатор транспортного средства
    /// </summary>
    public required int VehicleId { get; init; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; init; }

    /// <summary>
    /// Дата рейса
    /// </summary>
    public DateTime? Date { get; init; }

    /// <summary>
    /// Вес груза в тоннах
    /// </summary>
    public double? CargoWeight { get; init; }

    /// <summary>
    /// Стоимость перевозки
    /// </summary>
    public decimal? TransportationCost { get; init; }

    /// <summary>
    /// Статус рейса
    /// </summary>
    public TripStatus? Status { get; init; }

    /// <summary>
    /// Место отправления
    /// </summary>
    public required string Origin { get; init; }

    /// <summary>
    /// Место назначения
    /// </summary>
    public required string Destination { get; init; }
}
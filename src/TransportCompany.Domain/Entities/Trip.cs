using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entities;

/// <summary>
/// Рейс (поездка) транспортной компании
/// </summary>
public class Trip
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required int DriverId { get; set; }

    /// <summary>
    /// Идентификатор транспортного средства
    /// </summary>
    public required int VehicleId { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Пункт отправления
    /// </summary>
    /// <example>Москва</example>
    public required string Origin { get; set; }

    /// <summary>
    /// Пункт назначения
    /// </summary>
    /// <example>Санкт-Петербург</example>
    public required string Destination { get; set; }

    /// <summary>
    /// Дата рейса
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Вес груза в тоннах
    /// </summary>
    public double? CargoWeight { get; set; }

    /// <summary>
    /// Стоимость перевозки
    /// </summary>
    public decimal? TransportationCost { get; set; }

    /// <summary>
    /// Статус рейса
    /// </summary>
    public TripStatus? Status { get; set; }

    /// <summary>
    /// Водитель
    /// </summary>
    public Driver? Driver { get; set; }

    /// <summary>
    /// Транспортное средство
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public Client? Client { get; set; }
}
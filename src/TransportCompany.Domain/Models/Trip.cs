using TransportCompany.Domain.Shared.Enums;

namespace TransportCompany.Domain.Models;

/// <summary>
/// Рейс транспортного средства
/// </summary>
public class Trip
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Водитель
    /// </summary>
    public required Driver Driver { get; set; }

    /// <summary>
    /// Транспортное средство
    /// </summary>
    public required Vehicle Vehicle { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Пункт отправления
    /// </summary>
    public required string DeparturePoint { get; set; }

    /// <summary>
    /// Пункт назначения
    /// </summary>
    public required string DestinationPoint { get; set; }

    /// <summary>
    /// Дата отправления
    /// </summary>
    public DateOnly DepartureDate { get; set; }

    /// <summary>
    /// Вес груза в тоннах
    /// </summary>
    public double CargoWeight { get; set; }

    /// <summary>
    /// Стоимость перевозки
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Статус рейса
    /// </summary>
    public TripStatus Status { get; set; }

    /// <summary>
    /// Признак того, что рейс находится в процессе выполнения
    /// </summary>
    public bool IsActive => Status == TripStatus.InProgress;
}
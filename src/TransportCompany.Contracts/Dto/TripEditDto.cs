using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO для создания или редактирования рейса
/// </summary>
public class TripEditDto
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int DriverId { get; init; }

    /// <summary>
    /// Идентификатор транспортного средства
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int VehicleId { get; init; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int ClientId { get; init; }

    /// <summary>
    /// Дата рейса
    /// </summary>
    [Required]
    public required DateTime Date { get; init; }

    /// <summary>
    /// Вес груза в тоннах
    /// </summary>
    [Range(0.1, 100, ErrorMessage = "Вес груза должен быть положительным (от 0.1 до 100)")]
    public required double CargoWeight { get; init; }

    /// <summary>
    /// Стоимость перевозки
    /// </summary>
    [Range(0, 10000000, ErrorMessage = "Некорректная стоимость")]
    public decimal? TransportationCost { get; init; }

    /// <summary>
    /// Статус рейса
    /// </summary>
    public required TripStatus Status { get; init; }

    /// <summary>
    /// Пункт отправления
    /// </summary>
    [Required(ErrorMessage = "Пункт отправления обязателен")]
    [MaxLength(100)]
    public required string Origin { get; init; }

    /// <summary>
    /// Пункт назначения
    /// </summary>
    [Required(ErrorMessage = "Пункт назначения обязателен")]
    [MaxLength(100)]
    public required string Destination { get; init; }
}
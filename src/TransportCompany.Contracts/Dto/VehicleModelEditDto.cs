using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO для создания или редактирования модели транспортного средства
/// </summary>
public class VehicleModelEditDto
{
    /// <summary>
    /// Тип кузова
    /// </summary>
    [Range(0, 11, ErrorMessage = "Номер типа кузова должен быть от 0 до 11")]
    public required BodyType BodyType { get; init; }

    /// <summary>
    /// Объём кузова в м^3
    /// </summary>
    [Range(0, 200, ErrorMessage = "Объем кузова должен быть положительным (до 200 м^3)")]
    public required double BodyVolume { get; init; }
}
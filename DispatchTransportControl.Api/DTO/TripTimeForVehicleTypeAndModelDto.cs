using System.Text.Json.Serialization;
using DispatchTransportControl.Domain.Entity;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для суммарного времени поездок данного типа транспортного средства и модели
/// </summary>
public class TripTimeForVehicleTypeAndModelDto
{
    /// <summary>
    ///     Тип транспортного средства
    /// </summary>
    [JsonPropertyName("vehicle_type")]
    public required VehicleType VehicleType { get; set; }

    /// <summary>
    ///     Модель транспортного средства
    /// </summary>
    [JsonPropertyName("vehicle_model")]
    public required VehicleModelDto VehicleModelDto { get; set; }

    /// <summary>
    ///     Суммарное время поездок
    /// </summary>
    [JsonPropertyName("total_trip_time")]
    public required double TotalTripTime { get; set; }
}
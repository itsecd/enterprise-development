using System.Text.Json.Serialization;
using DispatchTransportControl.Domain.Entity;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для изменения транспортного средства
/// </summary>
public class VehicleUpdateDto
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Гос. номер транспортного средства
    /// </summary>
    [JsonPropertyName("registration_number")]
    public required string RegistrationNumber { get; set; }

    /// <summary>
    ///     Тип транспортного средства
    /// </summary>
    [JsonPropertyName("vehicle_type")]
    public required VehicleType VehicleType { get; set; }

    /// <summary>
    ///     Уникальный идентификатор модели транспортного средства
    /// </summary>
    [JsonPropertyName("vehicle_model_id")]
    public required int VehicleModelId { get; set; }

    /// <summary>
    ///     Год выпуска транспортного средства
    /// </summary>
    [JsonPropertyName("year_of_manufacture")]
    public required int YearOfManufacture { get; set; }
}
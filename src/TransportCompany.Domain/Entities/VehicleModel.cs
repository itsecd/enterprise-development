using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entities;

/// <summary>
/// Модель транспортного средства
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Тип кузова
    /// </summary>
    public required BodyType BodyType { get; set; }

    /// <summary>
    /// Объём кузова в м³
    /// </summary>
    public double? BodyVolume { get; set; }
}
using TransportCompany.Domain.Shared.Enums;

namespace TransportCompany.Domain.Models;

/// <summary>
/// Модель транспортного средства (справочник)
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Марка
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Название модели
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Грузоподъёмность в тоннах
    /// </summary>
    public double Capacity { get; set; }

    /// <summary>
    /// Требуемая категория водительских прав
    /// </summary>
    public LicenseCategory RequiredLicenseCategory { get; set; }

    /// <summary>
    /// Тип кузова
    /// </summary>
    public BodyType BodyType { get; set; }

    /// <summary>
    /// Объём кузова в кубических метрах
    /// </summary>
    public double BodyVolume { get; set; }
}
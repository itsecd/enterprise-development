namespace DispatchTransportControl.Domain;

/// <summary>
/// Модель транспортного средства
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название модели
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Низкопольная модель или нет
    /// </summary>
    public required bool LowFloor { get; set; }

    /// <summary>
    /// Максимальная вместимость
    /// </summary>
    public required int MaxCapacity { get; set; }
}
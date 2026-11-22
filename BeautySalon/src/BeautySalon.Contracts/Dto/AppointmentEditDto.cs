using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Запись на приём (для создания и изменения)
/// </summary>
public record AppointmentEditDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int ClientId { get; init; }

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int MasterId { get; init; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int ServiceId { get; init; }

    /// <summary>
    /// Дата и время приёма
    /// </summary>
    public required DateTime DateTime { get; init; }

    /// <summary>
    /// Признак постоянного клиента
    /// </summary>
    public bool IsRegularClient { get; init; }
}

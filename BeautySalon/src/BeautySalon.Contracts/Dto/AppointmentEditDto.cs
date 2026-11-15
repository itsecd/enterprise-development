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
    [Required, Range(1, int.MaxValue)]
    public int? ClientId { get; init; }

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    [Required, Range(1, int.MaxValue)]
    public int? MasterId { get; init; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    [Required, Range(1, int.MaxValue)]
    public int? ServiceId { get; init; }

    /// <summary>
    /// Дата и время приёма
    /// </summary>
    [Required]
    public DateTime? DateTime { get; init; }

    /// <summary>
    /// Признак постоянного клиента
    /// </summary>
    public bool IsRegularClient { get; init; }
}
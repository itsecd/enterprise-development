using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Contracts.Appointments;

/// <summary>
/// Данные записи для создания или обновления
/// </summary>
public sealed class SaveAppointmentRequest
{
    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int? CustomerId { get; init; }

    /// <summary>
    /// Идентификатор специалиста
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int? SpecialistId { get; init; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int? ServiceId { get; init; }

    /// <summary>
    /// Дата и время начала услуги по местному времени салона
    /// </summary>
    [Required]
    public DateTime? AppointmentDateTime { get; init; }

    /// <summary>
    /// Признак постоянного посетителя на момент записи
    /// </summary>
    [Required]
    public bool? IsRegularCustomer { get; init; }
}
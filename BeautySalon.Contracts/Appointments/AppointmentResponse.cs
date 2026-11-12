using BeautySalon.Contracts.Interfaces;

namespace BeautySalon.Contracts.Appointments;

/// <summary>
/// Сведения о записи посетителя
/// </summary>
public sealed class AppointmentResponse : IResourceResponse
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public required int CustomerId { get; init; }

    /// <summary>
    /// Идентификатор специалиста
    /// </summary>
    public required int SpecialistId { get; init; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int ServiceId { get; init; }

    /// <summary>
    /// Дата и время начала услуги по местному времени салона
    /// </summary>
    public required DateTime AppointmentDateTime { get; init; }

    /// <summary>
    /// Признак постоянного посетителя на момент записи
    /// </summary>
    public required bool IsRegularCustomer { get; init; }
}

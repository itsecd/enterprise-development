namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Данные для сохранения записи на услугу
/// </summary>
/// <param name="CustomerId">Идентификатор посетителя</param>
/// <param name="SpecialistId">Идентификатор специалиста</param>
/// <param name="ServiceId">Идентификатор услуги</param>
/// <param name="Start">Начало записи</param>
/// <param name="IsRegularCustomer">Признак постоянного посетителя</param>
public sealed record AppointmentDetails(
    int CustomerId,
    int SpecialistId,
    int ServiceId,
    DateTime Start,
    bool IsRegularCustomer);

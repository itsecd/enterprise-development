namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Услуга и количество записей на неё
/// </summary>
/// <param name="ServiceId">Идентификатор услуги</param>
/// <param name="Name">Название услуги</param>
/// <param name="AppointmentCount">Количество записей</param>
public sealed record ServicePopularity(int ServiceId, string Name, int AppointmentCount);

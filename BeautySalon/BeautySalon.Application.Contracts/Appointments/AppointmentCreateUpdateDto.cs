namespace BeautySalon.Application.Contracts.Appointments;

/// <summary>
/// Dto для put и post запросов к записям
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ServiceId">Идентификатор услуги</param>
/// <param name="MasterId">Идентификатор мастера</param>
/// <param name="StartTime">Время начала</param>
/// <param name="IsRegular">Постоянный клиент</param>
public record AppointmentCreateUpdateDto(int ClientId, int ServiceId, int MasterId, DateTime StartTime, bool IsRegular);

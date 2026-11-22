using BeautySalon.Contracts.Interfaces;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Запись на приём (для чтения)
/// </summary>
/// <param name="Id">Уникальный идентификатор</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientFullName">Полное имя клиента</param>
/// <param name="MasterId">Идентификатор мастера</param>
/// <param name="MasterFullName">Полное имя мастера</param>
/// <param name="ServiceId">Идентификатор услуги</param>
/// <param name="ServiceName">Название услуги</param>
/// <param name="DateTime">Дата и время приёма</param>
/// <param name="IsRegularClient">Признак постоянного клиента</param>
public record AppointmentDto(
    int Id,
    int ClientId,
    string ClientFullName,
    int MasterId,
    string MasterFullName,
    int ServiceId,
    string ServiceName,
    DateTime DateTime,
    bool IsRegularClient) : IHasId;
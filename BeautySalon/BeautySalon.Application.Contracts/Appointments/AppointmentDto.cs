using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;
using BeautySalon.Application.Contracts.Services;

namespace BeautySalon.Application.Contracts.Appointments;

/// <summary>
/// Dto для get запросов к записям
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Client">Dto клиента</param>
/// <param name="Service">Dto услуги</param>
/// <param name="Master">Dto мастера</param>
/// <param name="StartTime">Время начала</param>
/// <param name="IsRegular">Постоянный клиент</param>
public record AppointmentDto(int Id, ClientDto Client, ServiceDto Service, MasterDto Master, DateTime StartTime, bool IsRegular);

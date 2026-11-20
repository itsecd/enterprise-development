using VetClinic.Contracts.Dto.Appointments;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Сервис для работы с записями на прием
/// </summary>
public interface IAppointmentService : ICrudService<AppointmentDto, AppointmentEditDto>
{
}

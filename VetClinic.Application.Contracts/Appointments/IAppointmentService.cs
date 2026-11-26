namespace VetClinic.Application.Contracts.Appointments;

/// <summary>
/// Интерфейс прикладного сервиса для управления журналом записи на приемы
/// </summary>
public interface IAppointmentAppService : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <summary>
    /// Извлекает полную историю посещений клиники для конкретного питомца
    /// </summary>
    /// <param name="petId">Уникальный идентификатор питомца</param>
    /// <returns>Список записей на прием в виде DTO</returns>
    public Task<List<AppointmentDto>> GetAppointmentsByPetId(int petId);
}

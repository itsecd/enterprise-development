using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Appointments;
using BeautySalon.Domain;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Services;

/// <summary>
/// Служба для CRUD операций c записями
/// </summary>
/// <param name="appointmentRepository">Репозиторий записей</param>
/// <param name="clientRepository">Репозиторий клиентов</param>
/// <param name="serviceRepository">Репозиторий услуг</param>
/// <param name="masterRepository">Репозиторий мастеров</param>
public class AppointmentService(IRepository<Appointment, int> appointmentRepository,
    IRepository<Client, int> clientRepository,
    IRepository<Service, int> serviceRepository,
    IRepository<Master, int> masterRepository) : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<AppointmentDto> Create(AppointmentCreateUpdateDto dto)
    {
        var client = await clientRepository.Read(dto.ClientId);
        if (client == null) 
            throw new KeyNotFoundException($"Неверный Id клиента");

        var service = await serviceRepository.Read(dto.ServiceId);
        if (service == null)
            throw new KeyNotFoundException($"Неверный Id услуги");

        var master = await masterRepository.Read(dto.MasterId);
        if (master == null)
            throw new KeyNotFoundException($"Неверный Id мастера");

        var appointment = Mapper.Map(dto, client, service, master);

        var result = await appointmentRepository.Create(appointment);

        return Mapper.Map(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id) => await appointmentRepository.Delete(id);

    /// <inheritdoc/>
    public async Task<AppointmentDto?> Get(int id)
    {
        var appointment = await appointmentRepository.Read(id);

        if (appointment == null)
            return null;

        return Mapper.Map(appointment);
    }

    /// <inheritdoc/>
    public async Task<IList<AppointmentDto>> GetAll() => Mapper.Map(await appointmentRepository.ReadAll());

    /// <inheritdoc/>
    public async Task<AppointmentDto> Update(AppointmentCreateUpdateDto dto, int id)
    {
        var client = await clientRepository.Read(dto.ClientId);
        if (client == null)
            throw new KeyNotFoundException($"Неверный Id клиента");

        var service = await serviceRepository.Read(dto.ServiceId);
        if (service == null)
            throw new KeyNotFoundException($"Неверный Id услуги");

        var master = await masterRepository.Read(dto.MasterId);
        if (master == null)
            throw new KeyNotFoundException($"Неверный Id мастера");

        var appointment = Mapper.Map(dto, client, service, master);
        appointment.Id = id;

        var result = await appointmentRepository.Update(appointment);
        return Mapper.Map(result);
    }
}

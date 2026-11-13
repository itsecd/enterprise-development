using BeautySalon.Domain;
using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для записей
/// </summary>
public class AppointmentInMemoryRepository : IRepository<Appointment, int>
{
    private readonly List<Appointment> _appointments = DataSeeder.Appointments.ToList();
    private int _lastId = DataSeeder.Appointments.Count + 1;

    /// <inheritdoc/>
    public Task<Appointment> Create(Appointment entity)
    {
        entity.Id = _lastId++;
        _appointments.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var appointment = Read(entityId).Result;
        if (appointment == null)
            return Task.FromResult(false);

        var result = _appointments.Remove(appointment);
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<List<Appointment>> ReadAll()
    {
        return Task.FromResult(_appointments.ToList());
    }

    /// <inheritdoc/>
    public Task<Appointment?> Read(int entityId)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == entityId);
        return Task.FromResult(appointment);
    }

    /// <inheritdoc/>
    public Task<Appointment> Update(Appointment entity)
    {
        var appointment = Read(entity.Id).Result;

        if (appointment == null)
            return Create(entity);

        appointment.Client = entity.Client;
        appointment.Service = entity.Service;
        appointment.Master = entity.Master;
        appointment.StartTime = entity.StartTime;
        appointment.IsRegular = entity.IsRegular;

        return Task.FromResult(appointment);
    }
}

using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория записей.
/// </summary>
public class InMemoryAppointmentRepository : IAppointmentRepository
{
    private readonly List<Appointment> _appointments;

    /// <summary>
    /// Инициализирует репозиторий, копируя данные из SeederData.
    /// </summary>
    public InMemoryAppointmentRepository()
    {
        _appointments = SeederData.Appointments.ToList();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Appointment>> GetAllAsync() => Task.FromResult<IEnumerable<Appointment>>(_appointments);

    /// <inheritdoc />
    public Task<Appointment?> GetByIdAsync(int id) => Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));

    /// <inheritdoc />
    public Task AddAsync(Appointment entity)
    {
        entity.Id = _appointments.Count > 0 ? _appointments.Max(a => a.Id) + 1 : 1;
        _appointments.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Appointment entity) => Task.CompletedTask;

    /// <inheritdoc />
    public Task DeleteAsync(Appointment entity)
    {
        _appointments.Remove(entity);
        return Task.CompletedTask;
    }
}
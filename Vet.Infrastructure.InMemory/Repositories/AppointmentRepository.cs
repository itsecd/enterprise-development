using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.InMemory.Repositories;

/// <summary>
/// Класс репозитория для хранения данных в памяти
/// </summary>
/// <param name="data">Данные из датасида</param>
public class AppointmentRepository(VetClinicTestFixture data) : IRepository<Appointment, int>
{
    private readonly VetClinicTestFixture _data = data;
    private readonly Lock _lock = new();

    /// <inheritdoc />
    public Task<Appointment> AddAsync(Appointment entity)
    {
        lock (_lock)
        {
            var maxId = _data.Appointments.Count > 0 ? _data.Appointments.Max(p => p.Id) : 0;
            entity.Id = maxId + 1;

            _data.Appointments.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int entityId)
    {
        lock (_lock)
        {
            var appointment = _data.Appointments.FirstOrDefault(p => p.Id == entityId);
            if (appointment is null)
            {
                return Task.FromResult(false);
            }

            _data.Appointments.Remove(appointment);
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Appointment>> GetAllAsync()
    {
        lock (_lock)
        {
            IReadOnlyList<Appointment> appointments = _data.Appointments.ToList().AsReadOnly();
            return Task.FromResult(appointments);
        }
    }

    /// <inheritdoc />
    public Task<Appointment?> GetByIdAsync(int entityId)
    {
        lock (_lock)
        {
            var appointment = _data.Appointments.FirstOrDefault(p => p.Id == entityId);
            return Task.FromResult(appointment);
        }
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(Appointment entity)
    {
        lock (_lock)
        {
            var index = _data.Appointments.FindIndex(p => p.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _data.Appointments[index] = entity;
            return Task.FromResult(true);
        }
    }
}
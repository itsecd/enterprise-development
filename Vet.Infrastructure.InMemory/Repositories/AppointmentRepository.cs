using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.InMemory.Repositories;

/// <summary>
/// Класс репозитория для хранения данных в памяти
/// </summary>
/// <param name="data">Данные из датасида</param>
public class AppointmentRepository(VetClinicTestFixture data) : IAppointmentRepository
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

    /// <inheritdoc />
    public Task<IReadOnlyList<Pet>> GetDistinctPetsByVeterinarianIdAsync(int veterinarianId)
    {
        lock (_lock)
        {
            IReadOnlyList<Pet> result = _data.Appointments
                .Where(a => a.Veterinarian.Id == veterinarianId)
                .Select(a => a.Pet)
                .Distinct()
                .OrderBy(p => p.Name)
                .ToList()
                .AsReadOnly();
            return Task.FromResult(result);
        }
    }

    /// <inheritdoc />
    public Task<int> GetRepeatCountByBreedIdAsync(int breedId)
    {
        lock (_lock)
        {
            return Task.FromResult(_data.Appointments.Count(a => a.IsRepeat && a.Pet.Breed.Id == breedId));
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Appointment>> GetByRoomAndMonthAsync(int roomNumber, int year, int month)
    {
        lock (_lock)
        {
            IReadOnlyList<Appointment> result = _data.Appointments
                .Where(a => a.RoomNumber == roomNumber
                         && a.AppointmentDateTime.Year == year
                         && a.AppointmentDateTime.Month == month)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList()
                .AsReadOnly();
            return Task.FromResult(result);
        }
    }
}
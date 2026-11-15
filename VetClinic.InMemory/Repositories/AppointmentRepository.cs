using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.InMemory.Seeder;

namespace VetClinic.InMemory.Repositories;
/// <summary>
/// In memory репозиторий приёмов
/// </summary>
/// <param name="seeder">Сидер с начальными данными</param>
public class AppointmentRepository(VetClinicDataSeeder seeder) : IAppointmentRepository
{
    /// <inheritdoc />
    public Task<List<Appointment>> GetAll() => Task.FromResult(seeder.Appointments ?? []);

    /// <inheritdoc />
    public Task<Appointment?> Get(int id) => Task.FromResult(seeder.Appointments?.FirstOrDefault(x => x.Id == id));

    /// <inheritdoc />
    public Task Create(Appointment entity)
    {
        var list = seeder.Appointments ?? [];
        entity.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        seeder.Appointments?.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Update(Appointment entity)
    {
        var existing = seeder.Appointments?.FirstOrDefault(x => x.Id == entity.Id);
        if (existing == null) throw new InvalidOperationException($"Запись с ID {entity.Id} не найдена.");
        existing.DateTime = entity.DateTime;
        existing.Diagnosis = entity.Diagnosis;
        existing.RoomNumber = entity.RoomNumber;
        existing.RepeatVisitIndicator = entity.RepeatVisitIndicator;
        existing.PetId = entity.PetId;
        existing.Pet = entity.Pet;
        existing.VetId = entity.VetId;
        existing.Vet = entity.Vet;
        existing.ServiceId = entity.ServiceId;
        existing.Service = entity.Service;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Delete(int id)
    {
        var item = seeder.Appointments?.FirstOrDefault(x => x.Id == id);
        if (item != null) seeder.Appointments?.Remove(item);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<List<Appointment>> GetAppointmentsByPetId(int petId)
    {
        var filtered = (seeder.Appointments ?? [])
            .Where(a => a.PetId == petId)
            .ToList();
        return Task.FromResult(filtered);
    }
}

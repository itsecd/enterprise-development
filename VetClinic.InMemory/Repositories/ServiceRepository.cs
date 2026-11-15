using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.InMemory.Seeder;

namespace VetClinic.InMemory.Repositories;

/// <summary>
/// In Memory репозиторий услуг
/// </summary>
/// <param name="seeder">Сидер с начальными данными</param>
public class ServiceRepository(VetClinicDataSeeder seeder) : IRepository<Service>
{
    /// <inheritdoc />
    public Task<List<Service>> GetAll() => Task.FromResult(seeder.Services ?? []);

    /// <inheritdoc />
    public Task<Service?> Get(int id) => Task.FromResult(seeder.Services?.FirstOrDefault(x => x.Id == id));

    /// <inheritdoc />
    public Task Create(Service entity)
    {
        var list = seeder.Services ?? [];
        entity.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        seeder.Services?.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Update(Service entity)
    {
        var existing = seeder.Services?.FirstOrDefault(x => x.Id == entity.Id);
        if (existing == null) throw new InvalidOperationException($"Услуга с ID {entity.Id} не найдена.");
        existing.Title = entity.Title;
        existing.Price = entity.Price;
        existing.SpecializationId = entity.SpecializationId;
        existing.Specialization = entity.Specialization;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Delete(int id)
    {
        var item = seeder.Services?.FirstOrDefault(x => x.Id == id);
        if (item != null) seeder.Services?.Remove(item);
        return Task.CompletedTask;
    }
}

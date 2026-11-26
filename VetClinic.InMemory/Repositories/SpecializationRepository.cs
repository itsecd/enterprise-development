using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Seeder;

namespace VetClinic.InMemory.Repositories;

/// <summary>
/// In Memory репозиторий специализаций
/// </summary>
/// <param name="seeder">Сидер с начальными данными</param>
public class SpecializationRepository(VetClinicDataSeeder seeder) : IRepository<Specialization>
{
    /// <inheritdoc />
    public Task<List<Specialization>> GetAll() => Task.FromResult(seeder.Specializations ?? []);

    /// <inheritdoc />
    public Task<Specialization?> Get(int id) => Task.FromResult(seeder.Specializations?.FirstOrDefault(x => x.Id == id));

    /// <inheritdoc />
    public Task Create(Specialization entity)
    {
        var list = seeder.Specializations ?? [];
        entity.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        seeder.Specializations?.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Update(Specialization entity)
    {
        var existing = seeder.Specializations?.FirstOrDefault(x => x.Id == entity.Id);
        if (existing == null) throw new InvalidOperationException($"Специализация с ID {entity.Id} не найдена.");
        existing.Title = entity.Title;
        existing.TargetSpecies = entity.TargetSpecies;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Delete(int id)
    {
        var item = seeder.Specializations?.FirstOrDefault(x => x.Id == id);
        if (item != null) seeder.Specializations?.Remove(item);
        return Task.CompletedTask;
    }
}

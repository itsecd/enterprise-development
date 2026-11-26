using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.InMemory.Seeder;

namespace VetClinic.InMemory.Repositories;
/// <summary>
/// In memory репозиторий пород животных
/// </summary>
/// <param name="seeder">Сидер с начальными данными</param>
public class BreedRepository(VetClinicDataSeeder seeder) : IRepository<Breed>
{
    /// <inheritdoc />
    public Task<List<Breed>> GetAll() => Task.FromResult(seeder.Breeds ?? []);

    /// <inheritdoc />
    public Task<Breed?> Get(int id) => Task.FromResult(seeder.Breeds?.FirstOrDefault(x => x.Id == id));

    /// <inheritdoc />
    public Task Create(Breed entity)
    {
        var list = seeder.Breeds ?? [];
        entity.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        seeder.Breeds?.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Update(Breed entity)
    {
        var existing = seeder.Breeds?.FirstOrDefault(x => x.Id == entity.Id);
        if (existing == null) throw new InvalidOperationException($"Порода с ID {entity.Id} не найдена.");
        existing.Title = entity.Title;
        existing.Type = entity.Type;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Delete(int id)
    {
        var item = seeder.Breeds?.FirstOrDefault(x => x.Id == id);
        if (item != null) seeder.Breeds?.Remove(item);
        return Task.CompletedTask;
    }
}

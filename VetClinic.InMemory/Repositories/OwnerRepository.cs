using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.InMemory.Seeder;

namespace VetClinic.InMemory.Repositories;
/// <summary>
/// In memory репозиторий владельцев
/// </summary>
/// <param name="seeder">Сидер начальных данных</param>
public class OwnerRepository(VetClinicDataSeeder seeder) : IRepository<Owner>
{
    /// <inheritdoc />
    public Task<List<Owner>> GetAll() => Task.FromResult(seeder.Owners ?? []);

    /// <inheritdoc />
    public Task<Owner?> Get(int id) => Task.FromResult(seeder.Owners?.FirstOrDefault(x => x.Id == id));

    /// <inheritdoc />
    public Task Create(Owner entity)
    {
        var list = seeder.Owners ?? [];
        entity.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        seeder.Owners?.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Update(Owner entity)
    {
        var existing = seeder.Owners?.FirstOrDefault(x => x.Id == entity.Id);
        if (existing == null) throw new InvalidOperationException($"Владелец с ID {entity.Id} не найден.");
        existing.FullName = entity.FullName;
        existing.Phone = entity.Phone;
        existing.Adress = entity.Adress;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Delete(int id)
    {
        var item = seeder.Owners?.FirstOrDefault(x => x.Id == id);
        if (item != null) seeder.Owners?.Remove(item);
        return Task.CompletedTask;
    }
}

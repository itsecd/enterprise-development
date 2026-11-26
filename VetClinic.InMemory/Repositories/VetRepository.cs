using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Seeder;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.InMemory.Repositories;
/// <summary>
/// In-memory репозиторий врачей
/// </summary>
/// <param name="seeder">Сидер с начальными данными</param>
public class VetRepository(VetClinicDataSeeder seeder) : IVetRepository
{
    /// <inheritdoc />
    public Task<List<Vet>> GetAll() => Task.FromResult(seeder.Vets ?? []);

    /// <inheritdoc />
    public Task<Vet?> Get(int id) => Task.FromResult(seeder.Vets?.FirstOrDefault(x => x.Id == id));

    /// <inheritdoc />
    public Task Create(Vet entity)
    {
        var list = seeder.Vets ?? [];
        entity.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        seeder.Vets?.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Update(Vet entity)
    {
        var existing = seeder.Vets?.FirstOrDefault(x => x.Id == entity.Id);
        if (existing == null) throw new InvalidOperationException($"Врач с ID {entity.Id} не найден.");
        existing.FullName = entity.FullName;
        existing.PassportNumber = entity.PassportNumber;
        existing.BirthDate = entity.BirthDate;
        existing.ExpirienceYears = entity.ExpirienceYears;
        existing.SpecializationId = entity.SpecializationId;
        existing.Specialization = entity.Specialization;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Delete(int id)
    {
        var item = seeder.Vets?.FirstOrDefault(x => x.Id == id);
        if (item != null) seeder.Vets?.Remove(item);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<List<Vet>> FindVetsBySpecies(Species species)
    {
        var filtered = (seeder.Vets ?? [])
            .Where(v => v.Specialization.TargetSpecies == species)
            .ToList();
        return Task.FromResult(filtered);
    }
}

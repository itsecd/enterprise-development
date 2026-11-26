using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Seeder;

namespace VetClinic.InMemory.Repositories;

/// <summary>
/// In memory репозиторий питомцев
/// </summary>
/// <param name="seeder">Сидер с начальными данными</param>
public class PetRepository(VetClinicDataSeeder seeder) : IRepository<Pet>
{
    /// <inheritdoc />
    public Task<List<Pet>> GetAll() => Task.FromResult(seeder.Pets ?? []);

    /// <inheritdoc />
    public Task<Pet?> Get(int id) => Task.FromResult(seeder.Pets?.FirstOrDefault(p => p.Id == id));

    /// <inheritdoc />
    public Task Create(Pet entity)
    {
        var petsList = seeder.Pets ?? [];
        entity.Id = petsList.Any() ? petsList.Max(p => p.Id) + 1 : 1;
        seeder.Pets?.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Update(Pet entity)
    {
        var existingPet = seeder.Pets?.FirstOrDefault(p => p.Id == entity.Id);
        if (existingPet == null) throw new InvalidOperationException($"Питомец с ID {entity.Id} не найден.");

        existingPet.Name = entity.Name;
        existingPet.Weight = entity.Weight;
        existingPet.BirthDate = entity.BirthDate;
        existingPet.BreedId = entity.BreedId;
        existingPet.Breed = entity.Breed;
        existingPet.OwnerId = entity.OwnerId;
        existingPet.Owner = entity.Owner;
        existingPet.Type = entity.Type;

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task Delete(int id)
    {
        var pet = seeder.Pets?.FirstOrDefault(p => p.Id == id);
        if (pet != null) seeder.Pets?.Remove(pet);
        return Task.CompletedTask;
    }
}

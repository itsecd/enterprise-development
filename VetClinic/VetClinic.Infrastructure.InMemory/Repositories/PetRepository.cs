using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий питомцев в памяти
/// </summary>
/// <param name="seed">Начальные данные</param>
public class PetRepository(VetClinicSeed seed) : InMemoryRepository<Pet>(seed, seed.Pets), IPetRepository
{
    /// <inheritdoc/>
    protected override int GetId(Pet entity) => entity.Id;

    /// <inheritdoc/>
    protected override void SetId(Pet entity, int id) => entity.Id = id;

    /// <inheritdoc/>
    public Task<IReadOnlyList<Pet>> GetByVetOrderedByNicknameAsync(int vetId, CancellationToken cancellationToken = default)
    {
        var result = Seed.Pets
            .Where(pet => Seed.Appointments.Any(appointment =>
                (appointment.Pet == pet || appointment.PetId == pet.Id)
                && (appointment.Vet?.Id == vetId || appointment.VetId == vetId)))
            .OrderBy(pet => pet.Nickname)
            .ThenBy(pet => pet.Id)
            .ToList();

        return Task.FromResult<IReadOnlyList<Pet>>(result);
    }

    /// <inheritdoc/>
    public Task<bool> AnyByBreedAsync(int breedId, CancellationToken cancellationToken = default) =>
        Task.FromResult(Seed.Pets.Any(pet => pet.BreedId == breedId || pet.Breed?.Id == breedId));
}

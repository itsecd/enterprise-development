using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий владельцев в памяти
/// </summary>
/// <param name="seed">Начальные данные</param>
public class OwnerRepository(VetClinicSeed seed) : InMemoryRepository<Owner>(seed, seed.Owners), IOwnerRepository
{
    /// <inheritdoc/>
    protected override int GetId(Owner entity) => entity.Id;

    /// <inheritdoc/>
    protected override void SetId(Owner entity, int id) => entity.Id = id;

    /// <inheritdoc/>
    public Task<IReadOnlyList<OwnerPetsCount>> GetWithMultiplePetsOrderedByFullNameAsync(CancellationToken cancellationToken = default)
    {
        var result = Seed.Owners
            .Select(owner => new OwnerPetsCount(
                owner.Id,
                owner.FullName,
                Seed.Pets.Count(pet => pet.Owner == owner || pet.OwnerId == owner.Id)))
            .Where(owner => owner.PetsCount > 1)
            .OrderBy(owner => owner.FullName)
            .ThenBy(owner => owner.Id)
            .ToList();

        return Task.FromResult<IReadOnlyList<OwnerPetsCount>>(result);
    }
}

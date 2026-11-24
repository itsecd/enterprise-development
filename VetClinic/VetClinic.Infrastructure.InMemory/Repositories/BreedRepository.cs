using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий пород в памяти
/// </summary>
/// <param name="seed">Начальные данные</param>
public class BreedRepository(VetClinicSeed seed) : InMemoryRepository<Breed>(seed, seed.Breeds), IBreedRepository
{
    /// <inheritdoc/>
    protected override int GetId(Breed entity) => entity.Id;

    /// <inheritdoc/>
    protected override void SetId(Breed entity, int id) => entity.Id = id;
}

using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;
using VetClinic.Shared;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий ветеринаров в памяти
/// </summary>
/// <param name="seed">Начальные данные</param>
public class VetRepository(VetClinicSeed seed) : InMemoryRepository<Vet>(seed, seed.Vets), IVetRepository
{
    /// <inheritdoc/>
    protected override int GetId(Vet entity) => entity.Id;

    /// <inheritdoc/>
    protected override void SetId(Vet entity, int id) => entity.Id = id;

    /// <inheritdoc/>
    public Task<IReadOnlyList<Vet>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        var result = Seed.Vets
            .Where(vet => vet.Specialization != null && vet.Specialization.FocusSpecies == species)
            .OrderBy(vet => vet.Id)
            .ToList();

        return Task.FromResult<IReadOnlyList<Vet>>(result);
    }
}

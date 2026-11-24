using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий специализаций врачей в памяти
/// </summary>
/// <param name="seed">Начальные данные</param>
public class SpecializationRepository(VetClinicSeed seed) : InMemoryRepository<Specialization>(seed, seed.Specializations), ISpecializationRepository
{
    /// <inheritdoc/>
    protected override int GetId(Specialization entity) => entity.Id;

    /// <inheritdoc/>
    protected override void SetId(Specialization entity, int id) => entity.Id = id;
}

using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Хранилище справочника специализаций
/// </summary>
public interface ISpecializationRepository
{
    public Task<IReadOnlyList<Specialization>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<Specialization?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Specialization>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<Specialization> AddAsync(string name, AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<Specialization> UpdateAsync(int id, string name, AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default);
}

using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Хранилище справочника пород
/// </summary>
public interface IBreedRepository
{
    public Task<IReadOnlyList<Breed>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<Breed?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Breed>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<Breed> AddAsync(string name, AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<Breed> UpdateAsync(int id, string name, AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default);
}

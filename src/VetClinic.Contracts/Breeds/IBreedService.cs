using VetClinic.Domain.Enums;

namespace VetClinic.Contracts.Breeds;

public interface IBreedService
{
    public Task<IReadOnlyList<BreedDto>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<BreedDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<BreedDto>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<BreedDto> CreateAsync(BreedEditDto dto, CancellationToken cancellationToken = default);

    public Task<BreedDto> UpdateAsync(int id, BreedEditDto dto, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

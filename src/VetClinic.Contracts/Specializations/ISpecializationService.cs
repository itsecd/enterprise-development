using VetClinic.Domain.Enums;

namespace VetClinic.Contracts.Specializations;

public interface ISpecializationService
{
    public Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<SpecializationDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<SpecializationDto>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<SpecializationDto> CreateAsync(SpecializationEditDto dto, CancellationToken cancellationToken = default);

    public Task<SpecializationDto> UpdateAsync(int id, SpecializationEditDto dto, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

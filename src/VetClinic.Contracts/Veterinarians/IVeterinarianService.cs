using VetClinic.Domain.Enums;

namespace VetClinic.Contracts.Veterinarians;

public interface IVeterinarianService
{
    public Task<IReadOnlyList<VeterinarianDto>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<VeterinarianDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<VeterinarianDto>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<VeterinarianDto>> GetBySpecializationAsync(int specializationId, CancellationToken cancellationToken = default);

    public Task<VeterinarianDto> CreateAsync(VeterinarianEditDto dto, CancellationToken cancellationToken = default);

    public Task<VeterinarianDto> UpdateAsync(int id, VeterinarianEditDto dto, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

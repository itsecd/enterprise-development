namespace VetClinic.Contracts.Pets;

public interface IPetService
{
    public Task<IReadOnlyList<PetDto>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<PetDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<PetDto>> GetByOwnerAsync(int ownerId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<PetDto>> GetByVeterinarianAsync(int veterinarianId, CancellationToken cancellationToken = default);

    public Task<PetDto> CreateAsync(PetEditDto dto, CancellationToken cancellationToken = default);

    public Task<PetDto> UpdateAsync(int id, PetEditDto dto, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

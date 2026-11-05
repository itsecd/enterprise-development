namespace VetClinic.Contracts.Owners;

public interface IOwnerService
{
    public Task<IReadOnlyList<OwnerDto>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<OwnerDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<OwnerDto>> GetWithSeveralPetsAsync(CancellationToken cancellationToken = default);

    public Task<OwnerDto> CreateAsync(OwnerEditDto dto, CancellationToken cancellationToken = default);

    public Task<OwnerDto> UpdateAsync(int id, OwnerEditDto dto, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

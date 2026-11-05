using VetClinic.Domain.Entities;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Хранилище владельцев
/// </summary>
public interface IOwnerRepository
{
    public Task<IReadOnlyList<Owner>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<Owner?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Owner>> GetWithSeveralPetsAsync(CancellationToken cancellationToken = default);

    public Task<Owner> AddAsync(string fullName, string phoneNumber, string? address, CancellationToken cancellationToken = default);

    public Task<Owner> UpdateAsync(int id, string fullName, string phoneNumber, string? address, CancellationToken cancellationToken = default);

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default);
}

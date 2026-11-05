using VetClinic.Domain.Entities;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Хранилище питомцев
/// </summary>
public interface IPetRepository
{
    public Task<IReadOnlyList<Pet>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<Pet?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Pet>> GetByOwnerAsync(int ownerId, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Pet>> GetByVeterinarianAsync(int veterinarianId, CancellationToken cancellationToken = default);

    public Task<Pet> AddAsync(string name, int breedId, int ownerId, DateOnly birthDate, double weight, CancellationToken cancellationToken = default);

    public Task<Pet> UpdateAsync(int id, string name, int breedId, int ownerId, DateOnly birthDate, double weight, CancellationToken cancellationToken = default);

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default);
}

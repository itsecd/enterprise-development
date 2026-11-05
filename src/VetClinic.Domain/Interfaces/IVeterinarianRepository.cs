using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Хранилище ветеринаров
/// </summary>
public interface IVeterinarianRepository
{
    public Task<IReadOnlyList<Veterinarian>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<Veterinarian?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Veterinarian>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Veterinarian>> GetBySpecializationAsync(int specializationId, CancellationToken cancellationToken = default);

    public Task<Veterinarian> AddAsync(
        string passportNumber,
        string fullName,
        int specializationId,
        int birthYear,
        int experienceYears,
        CancellationToken cancellationToken = default);

    public Task<Veterinarian> UpdateAsync(
        int id,
        string passportNumber,
        string fullName,
        int specializationId,
        int birthYear,
        int experienceYears,
        CancellationToken cancellationToken = default);

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default);
}

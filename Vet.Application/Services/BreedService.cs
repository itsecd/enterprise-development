using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления породами
/// </summary>
/// <param name="breedRepository">Репозиторий пород</param>
public class BreedService(IRepository<Breed, int> breedRepository)
    : IApplicationService<BreedDto, BreedCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<BreedDto>> GetAllAsync()
    {
        var breeds = await breedRepository.GetAllAsync();
        return [.. breeds.Select(b => b.ToDto())];
    }

    /// <inheritdoc />
    public async Task<BreedDto?> GetByIdAsync(int id)
    {
        var breed = await breedRepository.GetByIdAsync(id);
        return breed?.ToDto();
    }

    /// <inheritdoc />
    public async Task<BreedDto> CreateAsync(BreedCreateUpdateDto dto)
    {
        var breed = dto.ToEntity();
        var created = await breedRepository.AddAsync(breed);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, BreedCreateUpdateDto dto)
    {
        var existing = await breedRepository.GetByIdAsync(id);
        if (existing is null)
        {
            return false;
        }

        existing.Name = dto.Name;
        existing.Species = dto.Species;

        return await breedRepository.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await breedRepository.DeleteAsync(id);
    }
}
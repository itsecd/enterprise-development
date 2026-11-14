using Microsoft.Extensions.Logging;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления породами
/// </summary>
/// <param name="breedRepository">Репозиторий пород</param>
/// <param name="petRepository">Репозиторий питомцев</param>
/// <param name="logger">Логгер сервиса</param>
public class BreedService(
    IRepository<Breed, int> breedRepository,
    IRepository<Pet, int> petRepository,
    ILogger<BreedService> logger)
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
        logger.LogInformation("Создана новая порода с Id {Id} ({Name})", created.Id, created.Name);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, BreedCreateUpdateDto dto)
    {
        var existing = await breedRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка обновления несуществующей породы с Id {Id}", id);
            return false;
        }

        existing.Name = dto.Name;
        existing.Species = dto.Species;

        var result = await breedRepository.UpdateAsync(existing);
        if (result)
        {
            logger.LogInformation("Порода с Id {Id} успешно обновлена", id);
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await breedRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка удаления несуществующей породы с Id {Id}", id);
            return false;
        }

        var pets = await petRepository.GetAllAsync();
        if (pets.Any(p => p.Breed.Id == id))
        {
            logger.LogWarning("Невозможно удалить породу с Id {Id}, так как на неё ссылаются питомцы", id);
            throw new InvalidOperationException($"Невозможно удалить породу с Id {id}, так как на неё ссылаются питомцы");
        }

        var result = await breedRepository.DeleteAsync(id);
        if (result)
        {
            logger.LogInformation("Порода с Id {Id} успешно удалена", id);
        }
        return result;
    }
}
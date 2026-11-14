using Microsoft.Extensions.Logging;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления специализациями
/// </summary>
/// <param name="specializationRepository">Репозиторий специализаций</param>
/// <param name="veterinarianRepository">Репозиторий ветеринаров</param>
/// <param name="logger">Логгер сервиса</param>
public class SpecializationService(
    IRepository<Specialization, int> specializationRepository,
    IRepository<Veterinarian, int> veterinarianRepository,
    ILogger<SpecializationService> logger)
    : IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync()
    {
        var specs = await specializationRepository.GetAllAsync();
        return [.. specs.Select(s => s.ToDto())];
    }

    /// <inheritdoc />
    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var spec = await specializationRepository.GetByIdAsync(id);
        return spec?.ToDto();
    }

    /// <inheritdoc />
    public async Task<SpecializationDto> CreateAsync(SpecializationCreateUpdateDto dto)
    {
        var spec = dto.ToEntity();
        var created = await specializationRepository.AddAsync(spec);
        logger.LogInformation("Создана новая специализация с Id {Id} ({Name})", created.Id, created.Name);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, SpecializationCreateUpdateDto dto)
    {
        var existing = await specializationRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка обновления несуществующей специализации с Id {Id}", id);
            return false;
        }

        existing.Name = dto.Name;
        existing.Species = [.. dto.Species];

        var result = await specializationRepository.UpdateAsync(existing);
        if (result)
        {
            logger.LogInformation("Специализация с Id {Id} успешно обновлена", id);
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await specializationRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка удаления несуществующей специализации с Id {Id}", id);
            return false;
        }

        var vets = await veterinarianRepository.GetAllAsync();
        if (vets.Any(v => v.Specialization.Id == id))
        {
            logger.LogWarning("Невозможно удалить специализацию с Id {Id}, так как она назначена ветеринарам", id);
            throw new InvalidOperationException($"Невозможно удалить специализацию с Id {id}, так как она назначена ветеринарам");
        }

        var result = await specializationRepository.DeleteAsync(id);
        if (result)
        {
            logger.LogInformation("Специализация с Id {Id} успешно удалена", id);
        }
        return result;
    }
}
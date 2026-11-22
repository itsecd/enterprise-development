using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Управление специализациями тренеров
/// </summary>
public class SpecializationService(
    IRepository<Specialization> specializationRepository,
    IRepository<Trainer> trainerRepository,
    ILogger<SpecializationService> logger) : ICrudService<SpecializationDto, SpecializationEditDto>
{
    /// <summary>
    /// Получить список специализаций
    /// </summary>
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var specializations = await specializationRepository.GetAllAsync(cancellationToken);
        return [.. specializations.Select(s => s.ToDto())];
    }
    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    public async Task<SpecializationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var specialization = await specializationRepository.GetByIdAsync(id, cancellationToken);
        return specialization?.ToDto();
    }
    /// <summary>
    /// Добавить специализацию
    /// </summary>
    public async Task<SpecializationDto> AddAsync(SpecializationEditDto editDto, CancellationToken cancellationToken = default)
    {
        var specialization = await specializationRepository.AddAsync(editDto.ToEntity());
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Создана специализация {SpecializationId}: {SpecializationName}", specialization.Id, specialization.Name);
        return specialization.ToDto();
    }
    /// <summary>
    /// Обновить специализацию
    /// </summary>
    public async Task<SpecializationDto?> UpdateAsync(int id, SpecializationEditDto editDto, CancellationToken cancellationToken = default)
    {
        var specialization = await specializationRepository.GetByIdAsync(id, cancellationToken);
        if (specialization is null)
        {
            logger.LogWarning("Специализация {SpecializationId} не найдена для обновления", id);
            return null;
        }
        specialization.UpdateEntity(editDto);
        await specializationRepository.UpdateAsync(specialization, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Обновлена специализация {SpecializationId}: {SpecializationName}", specialization.Id, specialization.Name);
        return specialization.ToDto();
    }
    /// <summary>
    /// Удалить специализацию при отсутствии связанных тренеров
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await trainerRepository.AnyAsync(t => t.Specialization.Id == id, cancellationToken))
            throw new ConflictException("Невозможно удалить специализацию, так как она назначена тренерам.");
        var deleted = await specializationRepository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {    
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Удалена специализация {SpecializationId}", id);
        }
        else
            logger.LogWarning("Специализация {SpecializationId} не найдена для удаления", id);
        return deleted;
    }
}
using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Application.Validation;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Управление тренерами
/// </summary>
public class TrainerService(
    IRepository<Trainer> trainerRepository,
    IRepository<Booking> bookingRepository,
    IRepository<Specialization> specializationRepository,
    PassportNumberUniquenessValidator passportNumberValidator,
    ILogger<TrainerService> logger) : ICrudService<TrainerDto, TrainerEditDto>
{
    /// <summary>
    /// Получить список тренеров
    /// </summary>
    public async Task<IReadOnlyList<TrainerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var trainers = await trainerRepository.GetAllAsync(cancellationToken);
        return [.. trainers.Select(t => t.ToDto())];
    }
    /// <summary>
    /// Получить тренера по идентификатору
    /// </summary>
    public async Task<TrainerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var trainer = await trainerRepository.GetByIdAsync(id, cancellationToken);
        return trainer?.ToDto();
    }
    /// <summary>
    /// Добавить тренера
    /// </summary>
    public async Task<TrainerDto> AddAsync(TrainerEditDto editDto, CancellationToken cancellationToken = default)
    {
        await passportNumberValidator.EnsureUniqueAsync(editDto.PassportNumber, cancellationToken: cancellationToken);
        var specialization = await specializationRepository.GetByIdAsync(editDto.SpecializationId, cancellationToken) 
            ?? throw new NotFoundException("Специализация не найдена.");
        var trainer = await trainerRepository.AddAsync(editDto.ToEntity(specialization), cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Создан тренер {TrainerId}: {FullName}", trainer.Id, trainer.FullName);
        return trainer.ToDto();
    }
    /// <summary>
    /// Обновить данные тренера
    /// </summary>
    public async Task<TrainerDto?> UpdateAsync(int id, TrainerEditDto editDto, CancellationToken cancellationToken = default)
    {
        var trainer = await trainerRepository.GetByIdAsync(id, cancellationToken);
        if (trainer is null)
        {
            logger.LogWarning("Тренер {TrainerId} не найден для обновления", id);
            return null;
        }
        await passportNumberValidator.EnsureUniqueAsync(editDto.PassportNumber, trainer, cancellationToken);
        var specialization = await specializationRepository.GetByIdAsync(editDto.SpecializationId, cancellationToken) 
            ?? throw new NotFoundException("Специализация не найдена.");
        trainer.UpdateEntity(editDto, specialization);
        await trainerRepository.UpdateAsync(trainer, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Обновлен тренер {TrainerId}: {FullName}", trainer.Id, trainer.FullName);
        return trainer.ToDto();
    }
    /// <summary>
    /// Удалить тренера при отсутствии бронирований
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await bookingRepository.AnyAsync(b => b.Trainer.Id == id, cancellationToken))
            throw new ConflictException("Невозможно удалить тренера, так как существуют бронирования для него.");
        var deleted = await trainerRepository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {    
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Удален тренер {TrainerId}", id);
        }
        else
            logger.LogWarning("Тренер {TrainerId} не найден для удаления", id);
        return deleted;
    }
}
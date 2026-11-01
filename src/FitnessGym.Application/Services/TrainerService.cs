using System.ComponentModel.DataAnnotations;
using FitnessGym.Application.Exceptions;
using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Операции с тренерами и преобразование данных в DTO.
/// </summary>
public class TrainerService(
    ITrainerRepository trainerRepository,
    ISpecializationRepository specializationRepository,
    ITrainingSessionRepository sessionRepository) : ITrainerService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<TrainerDto>> GetTrainersAsync() =>
        [.. (await trainerRepository.GetAllAsync()).Select(MapToDto)];

    /// <inheritdoc/>
    public async Task<TrainerDto?> GetTrainerAsync(int id)
    {
        var trainer = await trainerRepository.GetByIdAsync(id);
        return trainer is null ? null : MapToDto(trainer);
    }

    /// <inheritdoc/>
    public async Task<TrainerDto> AddTrainerAsync(UpsertTrainerDto trainer) =>
        MapToDto(await trainerRepository.AddAsync(await MapToEntityAsync(trainer)));

    /// <inheritdoc/>
    public async Task<TrainerDto?> UpdateTrainerAsync(int id, UpsertTrainerDto trainer)
    {
        if (await trainerRepository.GetByIdAsync(id) is null)
        {
            return null;
        }

        var entity = await MapToEntityAsync(trainer);
        if (!await trainerRepository.UpdateAsync(id, entity))
        {
            return null;
        }

        entity.Id = id;
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteTrainerAsync(int id)
    {
        if (await sessionRepository.HasTrainerAsync(id))
        {
            throw new EntityInUseException("У тренера есть записи на занятия — сначала удалите их.");
        }

        return await trainerRepository.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minYears)
    {
        if (minYears < 0)
        {
            throw new ValidationException("Минимальный стаж не может быть отрицательным.");
        }

        return [.. (await trainerRepository.GetExperiencedAsync(minYears)).Select(MapToDto)];
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<TrainerDto>> GetPopularTrainersAsync(int count)
    {
        if (count < 1)
        {
            throw new ValidationException("Количество тренеров должно быть положительным.");
        }

        return [.. (await sessionRepository.GetPopularTrainersAsync(count)).Select(MapToDto)];
    }

    private static TrainerDto MapToDto(Trainer trainer) => new()
    {
        Id = trainer.Id,
        PassportNumber = trainer.PassportNumber,
        FullName = trainer.FullName,
        Gender = trainer.Gender,
        BirthDate = trainer.BirthDate,
        SpecializationId = trainer.Specialization.Id,
        SpecializationName = trainer.Specialization.Name,
        ExperienceYears = trainer.ExperienceYears,
    };

    private async Task<Trainer> MapToEntityAsync(UpsertTrainerDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), validateAllProperties: true);
        return new Trainer
        {
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = dto.Gender
                ?? throw new ValidationException("Пол обязателен."),
            BirthDate = dto.BirthDate
                ?? throw new ValidationException("Дата рождения обязательна."),
            Specialization = await specializationRepository.GetByIdAsync(
                    dto.SpecializationId ?? throw new ValidationException("Специализация обязательна."))
                    ?? throw new ValidationException("Специализация не найдена."),
            ExperienceYears = dto.ExperienceYears,
        };
    }
}

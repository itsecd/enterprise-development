using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализует операции для работы с тренерами.
/// </summary>
public class TrainerService(
    ITrainerRepository trainerRepository,
    ISpecializationRepository specializationRepository) : ITrainerService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainerDto>> GetAllAsync()
    {
        var trainers = await trainerRepository.GetAllAsync();
        return [.. trainers.Select(trainer => trainer.ToDto())];
    }

    /// <inheritdoc />
    public async Task<TrainerDto?> GetByIdAsync(int id)
    {
        var trainer = await trainerRepository.GetByIdAsync(id);
        return trainer?.ToDto();
    }

    /// <inheritdoc />
    public async Task<TrainerDto> CreateAsync(CreateTrainerDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var specialization = await specializationRepository
            .GetByIdAsync(dto.SpecializationId);

        if (specialization is null)
        {
            throw new ArgumentException(
                "Указанная специализация не найдена.",
                nameof(dto));
        }

        var trainer = new Trainer
        {
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = (Gender)dto.Gender!.Value,
            BirthDate = dto.BirthDate,
            Specialization = specialization,
            WorkExperience = dto.WorkExperienceYears
        };

        var createdTrainer = await trainerRepository.AddAsync(trainer);
        return createdTrainer.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, UpdateTrainerDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var trainer = await trainerRepository.GetByIdAsync(id);

        if (trainer is null)
        {
            return false;
        }

        var specialization = await specializationRepository
            .GetByIdAsync(dto.SpecializationId);

        if (specialization is null)
        {
            throw new ArgumentException(
                "Указанная специализация не найдена.",
                nameof(dto));
        }

        trainer.PassportNumber = dto.PassportNumber;
        trainer.FullName = dto.FullName;
        trainer.Gender = (Gender)dto.Gender!.Value;
        trainer.BirthDate = dto.BirthDate;
        trainer.Specialization = specialization;
        trainer.WorkExperience = dto.WorkExperienceYears;

        return await trainerRepository.UpdateAsync(id, trainer);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        return trainerRepository.DeleteAsync(id);
    }
}
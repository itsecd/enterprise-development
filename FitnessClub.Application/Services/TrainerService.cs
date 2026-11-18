using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// CRUD-сервис тренеров
/// </summary>
public class TrainerService(
    ITrainerRepository trainerRepository,
    ISpecializationRepository specializationRepository)
    : CrudServiceBase<Trainer, TrainerDto, TrainerEditDto>(trainerRepository), ITrainerService
{
    /// <summary>
    /// Собирает сущность тренера, подгружая специализацию из репозитория
    /// </summary>
    /// <exception cref="KeyNotFoundException">Специализация не найдена</exception>
    protected override async Task<Trainer> MapToEntityAsync(TrainerEditDto editDto)
    {
        var specialization = await specializationRepository.Read(editDto.SpecializationId)
            ?? throw new KeyNotFoundException($"Специализация с идентификатором {editDto.SpecializationId} не найдена");

        return new Trainer
        {
            PassportNumber = editDto.PassportNumber,
            FullName = editDto.FullName,
            Gender = editDto.Gender is { } gender
                ? gender
                : throw new InvalidOperationException("Пол обязателен"),
            BirthDate = editDto.BirthDate,
            SpecializationId = editDto.SpecializationId,
            Specialization = specialization,
            ExperienceYears = editDto.ExperienceYears,
        };
    }

    /// <inheritdoc />
    protected override TrainerDto MapToDto(Trainer entity) => entity.ToDto();
}
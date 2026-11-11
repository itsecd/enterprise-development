using FitnessClub.Contracts.Dtos;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Выполняет преобразование тренера в DTO.
/// </summary>
public static class TrainerMapping
{
    /// <summary>
    /// Преобразует сущность тренера в DTO.
    /// </summary>
    /// <param name="trainer">Тренер фитнес-клуба.</param>
    /// <returns>DTO тренера.</returns>
    public static TrainerDto ToDto(this Trainer trainer)
    {
        ArgumentNullException.ThrowIfNull(trainer);

        return new TrainerDto
        {
            Id = trainer.Id,
            PassportNumber = trainer.PassportNumber,
            FullName = trainer.FullName,
            Gender = (GenderDto)trainer.Gender,
            BirthDate = trainer.BirthDate,
            SpecializationId = trainer.Specialization.Id,
            WorkExperienceYears = trainer.WorkExperience
        };
    }
}
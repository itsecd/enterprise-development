using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;
/// <summary>
/// Преобразует данные тренеров
/// </summary>
public static class TrainerMapper
{
    /// <summary>
    /// Преобразует тренера в DTO
    /// </summary>
    public static TrainerDto ToDto(this Trainer trainer) =>
        new(
            Id: trainer.Id,
            FullName: trainer.FullName,
            PassportNumber: trainer.PassportNumber,
            Gender: trainer.Gender,
            BirthDate: trainer.BirthDate,
            PhoneNumber: trainer.PhoneNumber,
            SpecializationId: trainer.Specialization.Id,
            SpecializationName: trainer.Specialization.Name,
            Experience: trainer.Experience);
    /// <summary>
    /// Создаёт тренера из DTO и специализации
    /// </summary>
    public static Trainer ToEntity(this TrainerEditDto trainerEditDto, Specialization specialization) =>
        new()
        {
            FullName = trainerEditDto.FullName,
            PassportNumber = trainerEditDto.PassportNumber,
            Gender = trainerEditDto.Gender,
            BirthDate = trainerEditDto.BirthDate,
            PhoneNumber = trainerEditDto.PhoneNumber,
            Specialization = specialization,
            Experience = trainerEditDto.Experience
        };
    /// <summary>
    /// Обновляет тренера данными из DTO и специализацией
    /// </summary>
    public static void UpdateEntity(this Trainer trainer, TrainerEditDto trainerEditDto, Specialization specialization)
    {
        trainer.FullName = trainerEditDto.FullName;
        trainer.PassportNumber = trainerEditDto.PassportNumber;
        trainer.Gender = trainerEditDto.Gender;
        trainer.BirthDate = trainerEditDto.BirthDate;
        trainer.PhoneNumber = trainerEditDto.PhoneNumber;
        trainer.Specialization = specialization;
        trainer.Experience = trainerEditDto.Experience;
    }
}
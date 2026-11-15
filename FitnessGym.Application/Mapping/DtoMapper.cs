using FitnessGym.Contracts.Dtos;
using FitnessGym.Domain.Entities;

namespace FitnessGym.Application.Mapping;

/// <summary>
/// Класс для преобразования доменных сущностей в DTO
/// </summary>
public static class DtoMapper
{
    /// <summary>
    /// Преобразовать тренера в DTO
    /// </summary>
    public static TrainerDto ToTrainerDto(Trainer t) => new(
        t.Id,
        t.PassportNumber,
        t.FullName,
        t.Gender.ToString(),
        t.BirthDate,
        t.Specialization.Id,
        t.Specialization.Name,
        t.ExperienceYears);

    /// <summary>
    /// Преобразовать запись на занятие в DTO
    /// </summary>
    public static TrainingSessionDto ToSessionDto(TrainingSession s) => new(
        s.Id,
        s.Client.Id,
        s.Client.FullName,
        s.Trainer.Id,
        s.Trainer.FullName,
        s.StartsTraining,
        s.EndsTraining,
        s.HallName.ToString(),
        s.IsTrial);
}
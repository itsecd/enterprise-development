using FitnessClub.Contracts.Dto;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Методы расширения для маппинга доменных сущностей в read-DTO
/// </summary>
public static class DtoMapper
{
    /// <summary>
    /// Маппинг специализации
    /// </summary>
    public static SpecializationDto ToDto(this Specialization specialization) => new()
    {
        Id = specialization.Id,
        Name = specialization.Name,
    };

    /// <summary>
    /// Маппинг клиента
    /// </summary>
    public static ClientDto ToDto(this Client client) => new()
    {
        Id = client.Id,
        PassportNumber = client.PassportNumber,
        FullName = client.FullName,
        Gender = client.Gender,
        BirthDate = client.BirthDate,
        Phone = client.Phone,
        SubscriptionStart = client.SubscriptionStart,
        SubscriptionEnd = client.SubscriptionEnd,
    };

    /// <summary>
    /// Маппинг тренера
    /// </summary>
    public static TrainerDto ToDto(this Trainer trainer) => new()
    {
        Id = trainer.Id,
        PassportNumber = trainer.PassportNumber,
        FullName = trainer.FullName,
        Gender = trainer.Gender,
        BirthDate = trainer.BirthDate,
        Specialization = trainer.Specialization.ToDto(),
        ExperienceYears = trainer.ExperienceYears,
    };

    /// <summary>
    /// Маппинг занятия
    /// </summary>
    public static LessonDto ToDto(this Lesson lesson) => new()
    {
        Id = lesson.Id,
        Client = lesson.Client.ToDto(),
        Trainer = lesson.Trainer.ToDto(),
        DateStart = lesson.DateStart,
        DateEnd = lesson.DateEnd,
        Hall = lesson.Hall,
        IsTrial = lesson.IsTrial,
    };
}
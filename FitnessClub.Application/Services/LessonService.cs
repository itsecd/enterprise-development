using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// CRUD-сервис занятий (записей клиентов на занятия)
/// </summary>
public class LessonService(
    ILessonRepository lessonRepository,
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository)
    : CrudServiceBase<Lesson, LessonDto, LessonEditDto>(lessonRepository), ILessonService
{
    /// <summary>
    /// Проверяет корректность времени занятия
    /// </summary>
    /// <exception cref="ArgumentException">Дата окончания не позже даты начала</exception>
    protected override Task ValidateAsync(LessonEditDto editDto)
    {
        if (editDto.DateEnd.HasValue && editDto.DateEnd.Value <= editDto.DateStart)
            throw new ArgumentException("Дата окончания занятия должна быть позже даты начала");

        return Task.CompletedTask;
    }

    /// <summary>
    /// Собирает сущность занятия, подгружая клиента и тренера из репозиториев
    /// </summary>
    /// <exception cref="KeyNotFoundException">Клиент или тренер не найдены</exception>
    protected override async Task<Lesson> MapToEntityAsync(LessonEditDto editDto)
    {
        var client = await clientRepository.Read(editDto.ClientId is { } clientId ? clientId : throw new InvalidOperationException("Идентификатор клиента обязателен"))
            ?? throw new KeyNotFoundException($"Клиент с идентификатором {editDto.ClientId} не найден");

        var trainer = await trainerRepository.Read(editDto.TrainerId is { } trainerId ? trainerId : throw new InvalidOperationException("Идентификатор тренера обязателен"))
            ?? throw new KeyNotFoundException($"Тренер с идентификатором {editDto.TrainerId} не найден");

        return new Lesson
        {
            ClientId = client.Id,
            Client = client,
            TrainerId = trainer.Id,
            Trainer = trainer,
            DateStart = editDto.DateStart,
            DateEnd = editDto.DateEnd,
            Hall = editDto.Hall,
            IsTrial = editDto.IsTrial,
        };
    }

    /// <inheritdoc />
    protected override LessonDto MapToDto(Lesson entity) => entity.ToDto();
}
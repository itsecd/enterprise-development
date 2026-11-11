using FitnessClub.Contracts.Dtos;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Выполняет преобразование записи на тренировку в DTO.
/// </summary>
public static class TrainingMapping
{
    /// <summary>
    /// Преобразует запись на тренировку в DTO.
    /// </summary>
    /// <param name="training">Запись на тренировку.</param>
    /// <returns>DTO тренировки.</returns>
    public static TrainingDto ToDto(this Training training)
    {
        ArgumentNullException.ThrowIfNull(training);

        return new TrainingDto
        {
            Id = training.Id,
            ClientId = training.Client.Id,
            TrainerId = training.Trainer.Id,
            HallId = training.Hall.Id,
            StartTime = training.StartTime,
            DurationMinutes = training.DurationMinutes,
            IsTrial = training.IsTrial
        };
    }
}
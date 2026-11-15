using FitnesClub.Api.Contracts.Trainers;

namespace FitnesClub.Api.Contracts.Analytics;

/// <summary>
/// Информация о популярном тренере с количеством проведенных занятий.
/// </summary>
/// <param name="Trainer">Данные тренера.</param>
/// <param name="BookingsCount">Количество записей на тренировки.</param>
public record PopularTrainerDto(
    TrainerDto Trainer,
    int BookingsCount
);
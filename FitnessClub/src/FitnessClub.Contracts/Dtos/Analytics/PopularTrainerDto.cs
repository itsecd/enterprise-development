namespace FitnessClub.Contracts.Dtos.Analytics;

/// <summary>
/// Данные о популярности тренера по количеству бронирований
/// </summary>
/// <param name="TrainerId">Идентификатор тренера</param>
/// <param name="TrainerFullName">Полное имя тренера</param>
/// <param name="BookingCount">Количество бронирований</param>
public record PopularTrainerDto(
    int TrainerId,
    string TrainerFullName,
    int BookingCount);

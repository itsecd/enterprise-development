namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления тренера и количества его записей
/// </summary>
/// <param name="Trainer">Информация о тренере</param>
/// <param name="SessionsCount">Количество записей на занятия к этому тренеру</param>
public record TrainerPopularityDto(
    TrainerDto Trainer,
    int SessionsCount);
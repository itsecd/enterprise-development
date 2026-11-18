namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Популярный тренер (аналитика)
/// </summary>
public class PopularTrainerDto
{
    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    public int TrainerId { get; init; }

    /// <summary>
    /// ФИО тренера
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Количество занятий
    /// </summary>
    public int LessonsCount { get; init; }
}
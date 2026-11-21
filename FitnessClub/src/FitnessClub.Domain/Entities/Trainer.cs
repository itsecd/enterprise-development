namespace FitnessClub.Domain.Entities;
/// <summary>
/// Класс данных для тренера фитнес клуба
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Специализация тренера
    /// </summary>
    public required Specialization Specialization { get; set; }
    /// <summary>
    /// Опыт работы в годах
    /// </summary>
    public int Experience { get; set; }
}
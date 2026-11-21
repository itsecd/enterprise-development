namespace FitnessClub.Domain.Entities;
/// <summary>
/// Класс специализации тренера
/// </summary>
public class Specialization : Entity
{
    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }
}
namespace FitnesClub.Domain.Entities;

/// <summary>
/// Специализация тренера
/// </summary>
public class Specialization
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название специализации
    /// </summary>
    /// <example>Фитнес-тренер</example>
    public required string Name { get; set; }

    /// <summary>
    /// Тренеры с данной специализацией
    /// </summary>
    public List<Trainer> Trainers { get; set; } = [];
}
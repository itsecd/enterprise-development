namespace FitnessClub.Domain.Entities;

/// <summary>
/// Зал фитнес-клуба.
/// </summary>
public class Gym
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название зала.
    /// </summary>
    public required string Name { get; set; }
}
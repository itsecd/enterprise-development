namespace FitnessClub.Domain.Entities;
/// <summary>
/// Класс зала тренировок
/// </summary>
public class Hall : Entity
{
    /// <summary>
    /// Название зала
    /// </summary>
    public required string Name { get; set; }
}
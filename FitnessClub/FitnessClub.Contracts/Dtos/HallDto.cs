namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные зала фитнес-клуба.
/// </summary>
public class HallDto
{
    /// <summary>
    /// ID зала.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название зала.
    /// </summary>
    /// <example>Тренажёрный зал №1</example>
    public required string Name { get; set; }

    /// <summary>
    /// Вместимость зала в количестве человек.
    /// </summary>
    public int Capacity { get; set; }
}
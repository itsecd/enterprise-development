namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Определяет сущность, имеющую ID.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// ID сущности.
    /// </summary>
    public int Id { get; set; }
}
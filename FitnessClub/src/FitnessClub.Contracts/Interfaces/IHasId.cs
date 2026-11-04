namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// У сушности есть идентификатор
/// </summary>
public interface IHasId
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public int Id { get; }
}
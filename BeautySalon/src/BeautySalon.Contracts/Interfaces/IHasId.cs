namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// DTO, у которого есть идентификатор
/// </summary>
public interface IHasId
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; }
}
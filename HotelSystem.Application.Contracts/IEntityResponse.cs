namespace HotelSystem.Application.Contracts;

/// <summary>
/// Ответ API с идентификатором сущности
/// </summary>
public interface IEntityResponse
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public int Id { get; }
}
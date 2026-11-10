namespace BeautySalon.Application.Exceptions;

/// <summary>
/// В переданных данных указан идентификатор несуществующей связанной сущности (в API превращается в ответ 400)
/// </summary>
public class ReferenceNotFoundException(string entityName, int id)
    : Exception($"Связанная сущность «{entityName}» с идентификатором {id} не найдена.")
{
    /// <summary>
    /// Название связанной сущности
    /// </summary>
    public string EntityName { get; } = entityName;

    /// <summary>
    /// Идентификатор, который не удалось найти
    /// </summary>
    public int EntityId { get; } = id;
}
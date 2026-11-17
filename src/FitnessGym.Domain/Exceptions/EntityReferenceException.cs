namespace FitnessGym.Domain.Exceptions;

/// <summary>
/// Операция нарушает связь между сущностями, например при одновременном удалении связанной записи.
/// </summary>
public class EntityReferenceException(Exception innerException)
    : Exception("Операция нарушает связи между записями. Обновите данные и повторите запрос.", innerException);

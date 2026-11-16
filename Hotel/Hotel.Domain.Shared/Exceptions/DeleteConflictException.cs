namespace Hotel.Domain.Shared.Exceptions;

/// <summary>
/// Запись нельзя удалить, пока на неё ссылаются другие записи
/// </summary>
public class DeleteConflictException(string message, Exception innerException)
    : Exception(message, innerException)
{
}
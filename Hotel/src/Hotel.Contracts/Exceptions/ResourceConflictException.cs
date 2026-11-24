namespace Hotel.Contracts.Exceptions;

/// <summary>
/// Операция противоречит существующим связям или уникальности данных
/// </summary>
public class ResourceConflictException(string message) : Exception(message)
{
}

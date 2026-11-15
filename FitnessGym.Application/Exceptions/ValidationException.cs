namespace FitnessGym.Application.Exceptions;

/// <summary>
/// Исключение, возникающее при ошибках валидации полей
/// </summary>
public class ValidationException(Dictionary<string, string[]> errors) : Exception("Ошибка валидации")
{
    public Dictionary<string, string[]> Errors { get; } = errors;
}
namespace VetClinic.Application.Exceptions;

/// <summary>
/// Порода принадлежит другому виду животных
/// </summary>
/// <param name="message">Описание ошибки</param>
public sealed class InvalidPetBreedException(string message) : Exception(message);

namespace VetClinic.Application.Exceptions;

/// <summary>
/// Изменение противоречит уже сохранённым данным
/// </summary>
/// <param name="message">Описание конфликта</param>
public class ConflictException(string message) : Exception(message);

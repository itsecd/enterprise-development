namespace FitnessGym.Application.Exceptions;

/// <summary>
/// Исключение, возникающее при некорректных входных данных от пользователя
/// </summary>
public class BadRequestException(string message) : Exception(message);
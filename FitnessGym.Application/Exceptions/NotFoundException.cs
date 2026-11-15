namespace FitnessGym.Application.Exceptions;

/// <summary>
/// Исключение, возникающее, когда запрашиваемый ресурс не найден
/// </summary>
public class NotFoundException(string message) : Exception(message);
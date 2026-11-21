namespace FitnessClub.Application.Exceptions;
/// <summary>
/// Исключение, выбрасываемое при не найденном ресурсе
/// </summary>
/// <param name="message">Сообщение об ошибке</param>
public class NotFoundException(string message) : Exception(message);
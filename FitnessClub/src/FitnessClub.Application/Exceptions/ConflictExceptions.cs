namespace FitnessClub.Application.Exceptions;
/// <summary>
/// Операция противоречит текущему состоянию данных
/// </summary>
/// <param name="message">Сообщение об ошибке</param>
public class ConflictException(string message) : Exception(message);
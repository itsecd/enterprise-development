namespace FitnessGym.Domain.Exceptions;

/// <summary>
/// Значение уникального поля уже используется другой записью.
/// </summary>
public class EntityConflictException(Exception innerException)
    : Exception("Запись с таким номером паспорта или названием специализации уже существует.", innerException);

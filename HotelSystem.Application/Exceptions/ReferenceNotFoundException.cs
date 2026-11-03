namespace HotelSystem.Application.Exceptions;

/// <summary>
/// Исключение, возникающее при ссылке на несуществующую сущность
/// </summary>
public class ReferenceNotFoundException(string entityName, int id)
    : Exception($"{entityName} с идентификатором {id} не найден");
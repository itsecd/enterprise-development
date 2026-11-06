namespace FoodDelivery.Application.Exceptions;

/// <summary>
/// Представляет отсутствие запрошенного ресурса
/// </summary>
/// <param name="message">Описание отсутствующего ресурса</param>
public sealed class ResourceNotFoundException(string message)
    : Exception(message);

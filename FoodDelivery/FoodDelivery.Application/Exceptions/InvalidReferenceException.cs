namespace FoodDelivery.Application.Exceptions;

/// <summary>
/// Представляет ссылку на отсутствующую связанную сущность
/// </summary>
/// <param name="message">Описание ошибки</param>
public sealed class InvalidReferenceException(string message)
    : Exception(message);

namespace FoodDelivery.Application.Exceptions;

/// <summary>
/// Представляет конфликт операции с текущим состоянием данных
/// </summary>
/// <param name="message">Описание конфликта</param>
public sealed class ResourceConflictException(string message)
    : Exception(message);

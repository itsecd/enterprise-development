namespace FoodDelivery.Domain.Exceptions;

/// <summary>
/// Представляет недопустимую стоимость позиции или заказа
/// </summary>
/// <param name="message">Описание ограничения</param>
public sealed class OrderAmountException(string message)
    : Exception(message);

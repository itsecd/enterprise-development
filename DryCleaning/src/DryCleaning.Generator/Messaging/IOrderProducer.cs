using DryCleaning.Contracts.Dto;

namespace DryCleaning.Generator.Messaging;

/// <summary>
/// Отправитель сгенерированных заказов на сервер
/// </summary>
public interface IOrderProducer
{
    /// <summary>
    /// Отправляет пачку заказов
    /// </summary>
    /// <param name="batch">Пачка заказов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task SendAsync(IList<OrderCreateDto> batch, CancellationToken cancellationToken);
}

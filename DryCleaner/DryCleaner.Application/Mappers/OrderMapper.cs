using DryCleaner.Application.Contracts.Order;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

/// <summary>
/// Преобразует сущности Order в объекты ProductDto или ProductDto в сущности Product.
/// </summary>
internal class OrderMapper
{

    /// <summary>
    /// Преобразует объект заказа в объект OrderDto.
    /// </summary>
    /// <param name="entity">Сущность Order для преобразования.</param>
    /// <returns>Экземпляр OrderDto, созданный на основе переданной сущности.</returns>
    public static OrderDto Map(Order entity)
    {
        return new
            (
            entity.Id,
            ClientMapper.Map(entity.Client),
            ProductMapper.Map(entity.Product),
            entity.AcceptedAt, entity.ProcessingDays,
            entity.Status
            );
    }

    /// <summary>
    /// Преобразует коллекцию сущностей Order в коллекцию объектов OrderDto.
    /// </summary>
    /// <param name="entity">Список сущностей Order для преобразования.</param>
    /// <returns>Список объектов OrderDto.</returns>
    public static IList<OrderDto> Map(IList<Order> entity)
    {
        return [.. entity.Select(Map)];
    }

    /// <summary>
    /// Создает новый экземпляр Order на основе данных из DTO, клиента и продукта.
    /// </summary>
    /// <param name="dto">Данные для создания или обновления заказа.</param>
    /// <param name="client">Клиент, связанный с заказом.</param>
    /// <param name="product">Продукт, связанный с заказом.</param>
    /// <returns>Новый экземпляр Order.</returns>
    public static Order Map(OrderCreateUpdateDto dto, Client client, Product product)
    {
        return new Order
        {
            Id = 0,
            Client = client,
            Product = product,
            AcceptedAt = dto.AcceptedAt,
            ProcessingDays = dto.ProcessingDays,
            Status = dto.Status
        };
    }
}

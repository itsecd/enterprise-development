using DryCleaner.Application.Contracts.Order;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Предоставляет сервис для управления заказами.
/// </summary>
/// <param name="orderRepository">Репозиторий для работы с сущностями заказов.</param>
/// <param name="clientRepository">Репозиторий для работы с сущностями клиентов.</param>
/// <param name="productRepository">Репозиторий для работы с сущностями изделий.</param>
public class OrderService(
    IRepository<Order, int> orderRepository,
    IRepository<Client, int> clientRepository,
    IRepository<Product, int> productRepository) : IOrderService
{
    /// <summary>
    /// Создает новый заказ на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для создания или обновления заказа.</param>
    /// <returns>Созданный заказ в виде объекта OrderDto.</returns>
    /// <exception cref="KeyNotFoundException">Выбрасывается, если клиент или изделие с указанным идентификатором не найдены.</exception>
    public async Task<OrderDto> Create(OrderCreateUpdateDto dto)
    {
        var client = await clientRepository.Read(dto.ClientId);
        if (client == null)
            throw new KeyNotFoundException($"Клиент с Id={dto.ClientId} не найден");

        var product = await productRepository.Read(dto.ProductId);
        if (product == null)
            throw new KeyNotFoundException($"Изделие с Id={dto.ProductId} не найдено");

        var order = OrderMapper.Map(dto, client, product);

        var result = await orderRepository.Create(order);

        return OrderMapper.Map(result);
    }

    /// <summary>
    /// Возвращает объект заказа по идентификатору или null, если заказ не найден.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <returns>Объект OrderDto, представляющий заказ, или null, если заказ не найден.</returns>
    public async Task<OrderDto?> Get(int id)
    {
        var order = await orderRepository.Read(id);

        if (order == null)
            return null;

        return OrderMapper.Map(order);
    }

    /// <summary>
    /// Получает все заказы в виде списка DTO.
    /// </summary>
    /// <returns>Список объектов OrderDto.</returns>
    public async Task<IList<OrderDto>> GetAll()
    {
        return OrderMapper.Map(await orderRepository.ReadAll());
    }

    /// <summary>
    /// Обновляет заказ на основе предоставленных данных и идентификатора.
    /// </summary>
    /// <param name="dto">Данные для обновления заказа.</param>
    /// <param name="id">Идентификатор обновляемого заказа.</param>
    /// <returns>Обновлённый заказ в виде OrderDto.</returns>
    /// <exception cref="KeyNotFoundException">Выбрасывается, если клиент или изделие не найдены.</exception>
    public async Task<OrderDto> Update(OrderCreateUpdateDto dto, int id)
    {
        var client = await clientRepository.Read(dto.ClientId);
        if (client == null)
            throw new KeyNotFoundException($"Клиент с Id={dto.ClientId} не найден");

        var product = await productRepository.Read(dto.ProductId);
        if (product == null)
            throw new KeyNotFoundException($"Изделие с Id={dto.ProductId} не найдено");

        var order = OrderMapper.Map(dto, client, product);
        order.Id = id;

        var result = await orderRepository.Update(order);

        return OrderMapper.Map(result);
    }

    /// <summary>
    /// Удаляет заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого заказа.</param>
    /// <returns>Значение true, если заказ успешно удалён; в противном случае — false.</returns>
    public async Task<bool> Delete(int id)
    {
        var result = await orderRepository.Delete(id);

        return result;
    }
}
